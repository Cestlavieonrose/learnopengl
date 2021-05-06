#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{
   // float depth = LinearizeDeth(gl_FragCoord.z) / far; // 为了演示除以 farr
    FragColor = vec4(texture(screenTexture, TexCoords).rgb, 1.0);
}
