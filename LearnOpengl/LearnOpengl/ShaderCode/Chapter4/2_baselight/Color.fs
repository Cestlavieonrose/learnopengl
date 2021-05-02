#version 330 core
out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;


in vec3 FragPos;
in vec3 Normal;


void main()
{
    //环境光
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
    //漫反射
    vec3 lightDir = normalize(lightPos-FragPos);
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0);
    vec3 diffColor = lightColor * diff;
    
    //高光反射
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffColor + specular)*objectColor;
    FragColor = vec4(result, 1.0);
}
