
     
    Shader "Hedgehog/Vegetation_D" {
	Properties
	{
		//_WavingTint ("Fade Color", Color) = (.7,.6,.5, 0)
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		//_WaveAndDistance ("Wave and distance", Vector) = (12, 3.6, 1, 1)
		_WaveAmplitude1 ("Wave Amplitude 1", float) = 1
		_WaveAmplitude2 ("Wave Amplitude 2", float) = 1
		_WaveFrequency1 ("Wave Frequency 1", float) = 0.5
		_WaveFrequency2 ("Wave Frequency 2", float) = 0.25
		_WindDirection ("Wind Direction", Vector) = (0.1, 0, 0.1, 0)	
	}

	SubShader
	{
		//Tags { "Queue" = "Geometry+200" "IgnoreProjector"="True" "RenderType"="Grass" }
		//Tags { "Queue" = "AlphaTest" "IgnoreProjector"="True" "RenderType"="Grass" }//MY MOD
		//Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}//MY MOD
		//Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	
	
	Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	
	
		Cull Off
		LOD 200
		//ColorMask RGB
			
		CGPROGRAM
		//#pragma surface surf Lambert vertex:vert addshadow noforwardadd approxview halfasview nolightmap
		//#pragma surface surf Lambert vertex:vert addshadow approxview halfasview 


		#pragma surface surf Lambert vertex:vert addshadow approxview halfasview 
		
		sampler2D _MainTex;
		fixed _Cutoff;
		fixed4 _Color;
		fixed _WaveAmplitude1;
		fixed _WaveAmplitude2;
		fixed _WaveFrequency1;
		fixed _WaveFrequency2;
		fixed4 _WindDirection;
		
		struct Input
		{
			float2 uv_MainTex;
			//float4 color : COLOR;
		};
		
		void FastSinCos (float4 val, out float4 s, out float4 c)
		{
			val = val * 6.408849 - 3.1415927;
			// powers for taylor series
			float4 r5 = val * val;
			float4 r6 = r5 * r5;
			float4 r7 = r6 * r5;
			float4 r8 = r6 * r5;
			float4 r1 = r5 * val;
			float4 r2 = r1 * r5;
			float4 r3 = r2 * r5;
			
			//Vectors for taylor's series expansion of sin and cos
			float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841};
			float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587};
			// sin
			s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
			// cos
			c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
		}
		
		void vert (inout appdata_full v)
		{
			
			float wind = _WaveAmplitude1 * sin(_WaveFrequency1 * 2 * 3.14 * _Time.y + v.vertex.x) + 
		                 _WaveAmplitude2 * cos(_WaveFrequency2 * 2 * 3.14 * _Time.y + v.vertex.y);
		
		    float split_v = 0.9; 
		    v.vertex = v.vertex + wind * v.texcoord.y * _WindDirection * (v.texcoord.y > split_v);
		}
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb* _Color.rgb;
			o.Alpha = c.a;
			clip (o.Alpha - _Cutoff);
		}
		ENDCG
	}
	Fallback Off
}
