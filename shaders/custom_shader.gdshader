shader_type spatial;
render_mode cull_disabled, specular_disabled, skip_vertex_transform, shadows_disabled;

uniform sampler2D albedo_color: source_color;
uniform sampler2D albedo_texture: source_color, filter_nearest;
uniform float jitter_snap_scale = 50.0f;

float snap(float value)
{
    return floor(value * jitter_snap_scale)/jitter_snap_scale;
}

void vertex() {
	VERTEX = (MODELVIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;
    NORMAL = normalize((MODELVIEW_MATRIX * vec4(NORMAL, 0.0)).xyz);
	// jitter
	VERTEX.x = snap(VERTEX.x);
	VERTEX.y = snap(VERTEX.y);
	VERTEX.z = snap(VERTEX.z);
}

void fragment() {
    // Sample the texture using the UV coordinates
    ALBEDO = texture(albedo_texture, UV).rgb;
}

void light() {
    DIFFUSE_LIGHT += ALBEDO * normalize(LIGHT_COLOR) * ATTENUATION;
}
