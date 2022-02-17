// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "BeatSaber/SM64BS"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_AdditiveColor("Additive Color", Color) = (0,0,0,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Glow ("Glow", Range (0, 1)) = 0
		_Ambient ("Ambient Lighting", Range (0, 1)) = 0
		_LightDir ("Light Direction", Vector) = (-1,-1,0,1)
		_Stretch ("Stretch", Vector) = (1,1,1,1)
		_VertexOffset("Vertex Offset", Vector) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 worldPos : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
				float3 normal : NORMAL;
				float4 color : COLOR;
				float4 diff : COLOR1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			float4 _Color;
			float4 _AdditiveColor;
			float _Glow;
			float _Ambient;
			float4 _LightDir;
			float4 _Stretch;
			float4 _VertexOffset;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				v.vertex = v.vertex * _Stretch + _VertexOffset;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldPos));
				o.normal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(o.normal, normalize(_LightDir.xyz)));
				o.diff = nl;
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);

				fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));

				col.rgb = _Color.rgb * (i.uv.x < .999 && i.uv.y < .999 ? lerp(i.color.rgb, col.rgb, col.a) : i.color.rgb);
				col = col * clamp(col * (_Ambient + i.diff), 0.0, 1.0);
				col.rgb += _AdditiveColor.rgb;

				return col * float4(1.0,1.0,1.0,_Glow);
			}
			ENDCG
		}
	}
}

