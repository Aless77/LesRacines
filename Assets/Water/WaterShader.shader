Shader "Unlit/WaterShader"
{

 Properties
    {
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _FlowDirection ("Flow Direction", Vector) = (1, 0, 0, 0)
        _Speed ("Flow Speed", Range(0.1, 2.0)) = 1.0
        _Color ("Water Color", Color) = (0.2, 0.5, 0.7, 0.8)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _NormalMap;
        float4 _FlowDirection;
        float _Speed;
        fixed4 _Color;

        struct Input
        {
            float2 uv_NormalMap;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Calculate flow using time and direction
            float2 flow = _FlowDirection.xy * (_Time.y * _Speed);

            // Offset texture coordinates for movement
            fixed4 normalTex = tex2D(_NormalMap, IN.uv_NormalMap + flow);

            // Apply water color
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;

            // Apply normal map for lighting effects (water surface waves)
            o.Normal = UnpackNormal(normalTex);
        }
        ENDCG
    }

    FallBack "Transparent/Diffuse"
}
