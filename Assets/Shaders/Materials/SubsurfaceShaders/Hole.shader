﻿Shader "Hole" {
	Properties {
		_MainTex ("Diffuse", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Geometry-1" } //draws before the Geometry

		ColorMask 0  //turns off color being drawn
		ZWrite off		//hole has no depth
		Stencil
		{
			Ref 1
			Comp always //compare what is in the stencil buffer and always writes a 1
			Pass replace
		}
		
		CGPROGRAM
		#pragma surface surf Lambert


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
