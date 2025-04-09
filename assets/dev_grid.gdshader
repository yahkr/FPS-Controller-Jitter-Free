shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_disabled, world_vertex_coords;

uniform bool solid = false;
uniform vec3 Floor : source_color;
uniform vec3 Wall : source_color;
uniform vec3 Lines : source_color;
uniform sampler2D Texture : repeat_enable, filter_nearest_mipmap_anisotropic;
uniform sampler2D depth_texture : hint_depth_texture, repeat_disable, filter_nearest;


// TextureParameterTriplanar
vec4 triplanar_texture(sampler2D p_sampler, vec3 p_weights, vec3 p_triplanar_pos) {
    vec4 samp = vec4(0.0);
    samp += texture(p_sampler, p_triplanar_pos.xy) * p_weights.z;
    samp += texture(p_sampler, p_triplanar_pos.xz) * p_weights.y;
    samp += texture(p_sampler, p_triplanar_pos.zy) * p_weights.x;
    return samp;
}


	uniform vec3 triplanar_scale = vec3(1.0, 1.0, 1.0);
	uniform vec3 triplanar_offset;
	uniform float triplanar_sharpness = 0.5;

	varying vec3 triplanar_power_normal;
	varying vec3 triplanar_pos;

void vertex() {
// TextureParameterTriplanar
	{
		triplanar_power_normal = pow(abs(NORMAL), vec3(triplanar_sharpness));
		triplanar_power_normal /= dot(triplanar_power_normal, vec3(1.0));
		triplanar_pos = VERTEX * triplanar_scale + triplanar_offset;
		triplanar_pos *= vec3(1.0, -1.0, 1.0);
	}
}

void fragment() {
	vec4 tex = triplanar_texture(Texture, triplanar_power_normal, triplanar_pos);
    float threshold = 0.9;

	vec3 worldNormal = (INV_VIEW_MATRIX * vec4(NORMAL, 0.0)).xyz;
	float dir = dot(worldNormal,vec3(0,1,0));

	vec3 world_camera = (INV_VIEW_MATRIX * vec4(vec3(0.0), 1.0)).xyz;
	vec4 a = INV_VIEW_MATRIX * vec4(VERTEX, 1.0);
	//ALBEDO = mix(vec3(1.0), vec3(0.0), smoothstep(0, 30, distance(a.xyz, world_camera)));
	vec3 color;
	color = tex.xyz * Lines;

	if(dir < 0.5f && !solid){
		color += (1.0-tex.xyz) * Wall;
	}else{
		color += (1.0-tex.xyz) * Floor;
	}



	ALBEDO = color;
}
