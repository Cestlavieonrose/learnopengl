#version 330 core
struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    vec3  position;
    vec3  direction;
    float cutOff;
    float outerCutOff;

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
    vec3 lightDir = normalize(light.position-FragPos);

    

    // 执行光照计算
    //环境光
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
    //漫反射

    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0);
    vec3 diffColor = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;

    //高光反射
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular =  light.specular * spec * texture(material.specular, TexCoords).rgb;
    
    //聚光 衰减
    
    // spotlight (soft edges)
    float theta = dot(lightDir, normalize(-light.direction));
    float epsilon = (light.cutOff - light.outerCutOff);
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);
    diffColor  *= intensity;
    specular *= intensity;
    
    // attenuation
    float distance    = length(light.position - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
    ambient  *= attenuation;
    diffColor   *= attenuation;
    specular *= attenuation;

    vec3 result = (ambient + diffColor + specular);
    FragColor = vec4(result, 1.0);
    
   
    
  
    
  
       
      
}
