#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{
   // float depth = LinearizeDeth(gl_FragCoord.z) / far; // 为了演示除以 farr
    FragColor = texture(texture1, TexCoords);
}
