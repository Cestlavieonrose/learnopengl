#version 330 core
out vec4 FragColor;

in vec3 Position;
in vec3 Normal;

uniform vec3 viewPos;

uniform samplerCube skybox;

void main()
{
    vec3 f = normalize(refract((Position - viewPos),Normal, 2.42));
    FragColor = texture(skybox, f);
}
