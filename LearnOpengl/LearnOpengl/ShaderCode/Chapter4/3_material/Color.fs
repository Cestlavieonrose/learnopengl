#version 330 core
struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

uniform Material material;

struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;


out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 viewPos;


in vec3 FragPos;
in vec3 Normal;


void main()
{
    //环境光
    vec3 ambient = light.ambient * material.ambient;
    //漫反射
    vec3 lightDir = normalize(light.position-FragPos);
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0);
    vec3 diffColor = light.diffuse * (diff * material.diffuse);
    
    //高光反射
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular =  light.specular * (spec * material.specular);

    vec3 result = (ambient + diffColor + specular);
    FragColor = vec4(result, 1.0);
}
