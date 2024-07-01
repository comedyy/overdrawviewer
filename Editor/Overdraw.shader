Shader "Hidden/DebugView/Overdraw"
{
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        Pass
        {
            ZWrite Off
            ZTest Always
            Blend One One

            CGPROGRAM           
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            float4 frag() : SV_TARGET 
            {
                return fixed4(0.1, 0.04, 0.02, 1);
            }
            ENDCG
        }
    }
}