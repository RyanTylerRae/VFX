struct CustomMain
{
    float time;
    float4 waveA;
    float4 waveB;
    float4 waveC;
    float4 waveD;
    float4 waveE;
    float4 waveF;

    float3 MyFunc(inout float3 normal, inout float3 tangent, inout float3 binormal, float3 position)
    {
        tangent = float3(1.0f, 0.0f, 0.0f);
        binormal = float3(0.0f, 1.0f, 0.0f);
        float3 p = float3(0.0f, 0.0f, 0.0f);
        p += GertsnerWave(waveA, position, tangent, binormal);
        p += GertsnerWave(waveB, position, tangent, binormal);
        p += GertsnerWave(waveC, position, tangent, binormal);
        p += GertsnerWave(waveD, position, tangent, binormal);
        p += GertsnerWave(waveE, position, tangent, binormal);
        p += GertsnerWave(waveF, position, tangent, binormal);
        normal = normalize(cross(tangent, binormal));
        return p;
    }

    float3 GertsnerWave(float4 wave, float3 pos, inout float3 tangent, inout float3 binormal)
    {
        float invWavelength = wave.x;
        float steepness = wave.y;
        float2 direction = normalize(wave.zw);

        float k = 2.0f * PI * invWavelength;
        float c = sqrt(9.8f / k);
        float f = k * (dot(direction, pos.xy) - c * time);
        float a = steepness / k;

        float dx = direction.x;
        float dy = direction.y;
        float dx2 = dx * dx;
        float dy2 = dy * dy;

        tangent += float3(-dx2 * steepness * sin(f), -dx * dy * steepness * sin(f), dx * steepness * cos(f));
        binormal += float3(-dx * dy * steepness * sin(f), -dy2 * steepness * sin(f), dy * steepness * cos(f));

        return float3(direction.x * a * cos(f), direction.y * a * cos(f), a * sin(f));
    }
};

CustomMain CM;
CM.time = Time;
CM.waveA = WaveA;
CM.waveB = WaveB;
CM.waveC = WaveC;
CM.waveD = WaveD;
CM.waveE = WaveE;
CM.waveF = WaveF;

return CM.MyFunc(Normal, Tangent, Binormal, Position);
