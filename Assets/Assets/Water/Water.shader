Shader "Custom/StylizedWater"
{
    Properties
    {
        _ShallowColor ("Shallow Water", Color) = (0.55,0.75,0.9,1)
        _DeepColor ("Deep Water", Color) = (0.2,0.4,0.6,1)

        _FoamColor ("Foam Color", Color) = (0.95,0.97,1,1)

        _WaveScale ("Wave Scale", Float) = 2
        _WaveSpeed ("Wave Speed", Float) = 0.6
        _WaveStrength ("Wave Strength", Float) = 0.05

        _FoamDistance ("Foam Distance", Float) = 0.4
        _DepthDistance ("Depth Distance", Float) = 5

        _GlitterStrength ("Glitter Strength", Float) = 1
        _GlitterScale ("Glitter Scale", Float) = 15

        _ShoreWaveSpeed ("Shore Wave Speed", Float) = 2
        _ShoreWaveFrequency ("Shore Wave Frequency", Float) = 15

        _FlowStrength ("Surface Flow Strength", Float) = 0.15
        _FlowSpeed ("Surface Flow Speed", Float) = 0.25

        _Alpha ("Transparency", Range(0,1)) = 0.85
    }

    SubShader
    {
        Tags{ "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _ShallowColor;
            float4 _DeepColor;
            float4 _FoamColor;

            float _WaveScale;
            float _WaveSpeed;
            float _WaveStrength;

            float _FoamDistance;
            float _DepthDistance;

            float _GlitterStrength;
            float _GlitterScale;

            float _ShoreWaveSpeed;
            float _ShoreWaveFrequency;

            float _FlowStrength;
            float _FlowSpeed;

            float _Alpha;

            sampler2D _CameraDepthTexture;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };

            float noise(float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233))) * 43758.5453);
            }

            // Directional moving waves
            float waveHeight(float2 p)
            {
                float2 dir = normalize(float2(1,0.6));

                float travel = dot(p,dir);

                float w1 = sin(travel * _WaveScale + _Time.y * _WaveSpeed);
                float w2 = sin(travel * (_WaveScale*0.6) + _Time.y * _WaveSpeed*0.7);

                return (w1+w2)*0.5;
            }

            v2f vert(appdata v)
            {
                v2f o;

                float3 pos = v.vertex.xyz;

                float3 world = mul(unity_ObjectToWorld,v.vertex).xyz;

                float wave = waveHeight(world.xz);

                pos.y += wave * _WaveStrength;

                o.pos = UnityObjectToClipPos(float4(pos,1));

                o.worldPos = mul(unity_ObjectToWorld,float4(pos,1)).xyz;

                float eps = 0.2;

                float h1 = waveHeight(world.xz + float2(eps,0));
                float h2 = waveHeight(world.xz + float2(0,eps));

                float3 dx = float3(eps,(h1-wave)*_WaveStrength,0);
                float3 dz = float3(0,(h2-wave)*_WaveStrength,eps);

                o.normal = normalize(cross(dz,dx));

                o.screenPos = ComputeScreenPos(o.pos);

                return o;
            }

            float getDepth(float4 screenPos)
            {
                float2 uv = screenPos.xy / screenPos.w;
                return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,uv);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);

                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

                float fresnel = pow(1 - dot(viewDir,normal),4);

                float sceneDepth = LinearEyeDepth(getDepth(i.screenPos));
                float waterDepth = i.screenPos.w;

                float depthDiff = sceneDepth - waterDepth;

                // Depth-based water color

                float depthFactor = saturate(depthDiff / _DepthDistance);

                float3 waterColor = lerp(_ShallowColor.rgb,_DeepColor.rgb,depthFactor);

                // Shore foam

                float foam = saturate(1 - depthDiff/_FoamDistance);
                foam = smoothstep(0.3,0.6,foam);

                // Shoreline wave animation

                float shoreMask = saturate(1 - depthDiff / (_FoamDistance*2));

                float shoreWave =
                    sin(depthDiff * _ShoreWaveFrequency - _Time.y * _ShoreWaveSpeed);

                shoreWave = smoothstep(0.4,0.6,shoreWave);

                float shoreFoam = shoreWave * shoreMask;

                // Surface flow distortion

                float2 flow = i.worldPos.xz;

                float2 flowDir = normalize(float2(0.8,0.3));

                flow += flowDir * _Time.y * _FlowSpeed;

                float flowNoise = noise(flow * 3);

                float2 flowOffset = flowNoise * _FlowStrength;

                float2 glitterUV = i.worldPos.xz + flowOffset;

                // Glitter linked to waves

                float wave = waveHeight(i.worldPos.xz);

                float crest = saturate(wave * 3);

                float slope = 1 - normal.y;

                float g = pow(noise(glitterUV * _GlitterScale),14);

                float glitter = g * crest * slope * fresnel * _GlitterStrength;

                // Final color

                float3 col = lerp(waterColor,float3(1,1,1),fresnel*0.35);

                col = lerp(col,_FoamColor.rgb,foam);

                col = lerp(col,_FoamColor.rgb,shoreFoam);

                col += foam * 0.25;

                col += glitter;

                return float4(col,_Alpha);
            }

            ENDHLSL
        }
    }
}