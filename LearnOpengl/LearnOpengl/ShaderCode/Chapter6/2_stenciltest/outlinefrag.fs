#version 330 core
out vec4 FragColor;

void main()
{
   // float depth = LinearizeDepth(gl_FragCoord.z) / far; // 为了演示除以 farr
    FragColor = vec4(0.04, 0.28, 0.26, 1.0);
   // FragColor = vec4(texture(texture1, TexCoords).rgb, 1.0);

}
