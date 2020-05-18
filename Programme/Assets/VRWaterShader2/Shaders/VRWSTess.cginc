#ifdef UNITY_CAN_COMPILE_TESSELLATION
    struct TessVertex {
        float4 vertex : INTERNALTESSPOS;
        float3 normal : NORMAL;
        float4 tangent : TANGENT;
        float2 texcoord0 : TEXCOORD0;
        float2 texcoord1 : TEXCOORD1;
        float2 texcoord2 : TEXCOORD2;
    };
    struct OutputPatchConstant {
        float edge[3]         : SV_TessFactor;
        float inside          : SV_InsideTessFactor;
        float3 vTangent[4]    : TANGENT;
        float2 vUV[4]         : TEXCOORD;
        float3 vTanUCorner[4] : TANUCORNER;
        float3 vTanVCorner[4] : TANVCORNER;
        float4 vCWts          : TANWEIGHTS;
    };
    TessVertex tessvert (VertexInput v) {
        TessVertex o;
        o.vertex = v.vertex;
        o.normal = v.normal;
        o.tangent = v.tangent;
        o.texcoord0 = v.texcoord0;
        o.texcoord1 = v.texcoord1;
        o.texcoord2 = v.texcoord2;
        return o;
    }
    void displacement (inout VertexInput v){
        float time = _Time.g;

        // FIRST
        float2 first_uv_pos = (v.texcoord0+(float2(_FirstWaveUandVspeed.r,_FirstWaveUandVspeed.g)*time));
        float4 _FirstWaveHeightmap_var = tex2Dlod(_FirstWaveHeightmap,float4(TRANSFORM_TEX(first_uv_pos, _FirstWaveNormalmap),0.0,0));
        float3 firstWaveHeight = (_FirstWaveHeightmap_var.rgb-0.5)*_FirstWaveHeightStrength;

        // SECOND
        float3 secondWaveHeight = float3(0,0,0);
        #ifdef SECOND_ENABLED
            float2 second_uv_pos = (v.texcoord0+(float2(_SecondWaveUandVspeed.r,_SecondWaveUandVspeed.g)*time));
            float4 _SecondWaveHeightmap_var = tex2Dlod(_SecondWaveHeightmap,float4(TRANSFORM_TEX(second_uv_pos, _SecondWaveNormalmap),0.0,0));
            secondWaveHeight = (_SecondWaveHeightmap_var.rgb-0.5)*_SecondWaveHeightStrength;
        #endif

        // THIRD
        float3 thirdWaveHeight = float3(0,0,0);
        #ifdef THIRD_ENABLED
            float2 third_uv_pos = (v.texcoord0+(float2(_ThirdWaveUandVspeed.r,_ThirdWaveUandVspeed.g)*time));
            float4 _ThirdWaveHeightmap_var = tex2Dlod(_ThirdWaveHeightmap,float4(TRANSFORM_TEX(third_uv_pos, _ThirdWaveNormalmap),0.0,0));
            thirdWaveHeight = (_ThirdWaveHeightmap_var.rgb-0.5)*_ThirdWaveHeightStrength;
        #endif

        // FOURTH
        float3 fourthWaveHeight = float3(0,0,0);
        #ifdef FOURTH_ENABLED
            float2 fourth_uv_pos = (v.texcoord0+(float2(_FourthWaveUandVspeed.r,_FourthWaveUandVspeed.g)*time));
            float4 _FourthWaveHeightmap_var = tex2Dlod(_FourthWaveHeightmap,float4(TRANSFORM_TEX(fourth_uv_pos, _FourthWaveNormalmap),0.0,0));
            fourthWaveHeight = (_FourthWaveHeightmap_var.rgb-0.5)*_FourthWaveHeightStrength;
        #endif

        float _OverallWaveHeightMask_var = tex2Dlod(_OverallWaveHeightMask,float4(TRANSFORM_TEX(v.texcoord0, _OverallWaveHeightMask),0,0 )) .r;
        float _OverallWaveHeight_var = _OverallWaveHeight * _OverallWaveHeightMask_var;
        v.vertex.xyz += ( (firstWaveHeight + secondWaveHeight + thirdWaveHeight + fourthWaveHeight) * v.normal * _OverallWaveHeight_var);
    }
    float Tessellation(TessVertex v){
        return max(1.0,(((distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos)-_TessellationFarCap)/(_TessellationNearCap-_TessellationFarCap))*_TessellationStrength));
    }
    float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
        float tv = Tessellation(v);
        float tv1 = Tessellation(v1);
        float tv2 = Tessellation(v2);
        return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
    }
    OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
        OutputPatchConstant o = (OutputPatchConstant)0;
        float4 ts = Tessellation( v[0], v[1], v[2] );
        o.edge[0] = ts.x;
        o.edge[1] = ts.y;
        o.edge[2] = ts.z;
        o.inside = ts.w;
        return o;
    }
    [domain("tri")]
    [partitioning("fractional_odd")]
    [outputtopology("triangle_cw")]
    [patchconstantfunc("hullconst")]
    [outputcontrolpoints(3)]
    TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
        return v[id];
    }
    [domain("tri")]
    VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
        VertexInput v = (VertexInput)0;
        v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
        v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
        v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
        v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
        v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
        displacement(v);
        VertexOutput o = vert(v);
        return o;
    }
#endif