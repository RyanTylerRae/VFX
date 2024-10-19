float3 xyz = float3(0.0f, 0.0f, 0.0f);
float2 direction = normalize(Direction);

xyz = Position;

float k = 2.0f * PI * InvWavelength;
float c = sqrt(9.8f / k);
float f = k * (dot(direction, xyz.xy) - c * Time);
float a = Steepness / k;

xyz.x += direction.x * a * cos(f);
xyz.y += direction.y * a * cos(f);
xyz.z = a * sin(f);

float dx = direction.x;
float dy = direction.y;
float dx2 = dx * dx;
float dy2 = dy * dy;

Tangent = normalize(float3(1.0f - dx2 * Steepness * sin(f), -dx * dy * Steepness * sin(f), dx * Steepness * cos(f)));
Binormal = normalize(float3(-dx * dy * Steepness * sin(f), 1.0f - dy2 * Steepness * sin(f), dy * Steepness * cos(f)));
Normal = normalize(cross(Tangent, Binormal));

return xyz;
