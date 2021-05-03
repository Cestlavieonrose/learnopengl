#version 330 core
struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    // vec3 position; // 使用定向光就不再需要了
    //vec3 direction;
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    
    float constant;
    float linear;
    float quadratic;
};



out vec4 FragColor;

uniform vec3 viewPos;
uniform Light light;
uniform Material material;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;


void main()
{
    //点光源衰减度
    float distance    = length(light.position - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance +
                    light.quadratic * (distance * distance));
    
    //环境光
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
    //漫反射

    vec3 lightDir = normalize(light.position-FragPos);
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0);
    vec3 diffColor = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;
    
    //高光反射
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular =  light.specular * spec * texture(material.specular, TexCoords).rgb;

    vec3 result = (ambient + diffColor + specular)*attenuation;
    FragColor = vec4(result, 1.0);
    
  
       
      
}
