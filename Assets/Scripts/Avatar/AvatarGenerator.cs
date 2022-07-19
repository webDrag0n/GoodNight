using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarGenerator : MonoBehaviour
{
    public SpriteRenderer head_renderer;
    public SpriteRenderer body_renderer;
    public SpriteRenderer legs_renderer;
    

    public Sprite[] heads;
    public Sprite[] body;
    public Sprite[] legs;

    public void RenderAvatar(int x, int y, int z)
    {
        Sprite[] avatar = GetSpecificAvatar(x, y, z);
        head_renderer.sprite = avatar[0];
        body_renderer.sprite = avatar[1];
        legs_renderer.sprite = avatar[2];
    }

    public Sprite[] GetSpecificAvatar(int x, int y, int z)
    {
        Sprite[] avatar = { heads[x], body[y], legs[z] };
        return avatar;
    }
    
    // [head, body, leg]
    public Sprite[] GetRandomAvatar()
    {
        Sprite[] avatar = {
            heads[Random.Range(0, heads.Length)],
            body[Random.Range(0, heads.Length)],
            legs[Random.Range(0, heads.Length)]
        };
        return avatar;
    }
}
