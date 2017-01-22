using UnityEngine;

public class Characters{

    string name;
    Texture avatar;
    Texture boxImage;

    public void SetName(string name) {
        this.name = name;
    }

    public string GetName() {
        return this.name;
    }
    public void SetAvatar(Texture avatar) {
        this.avatar = avatar;
    }
    public Texture GetAvatar() {
        return this.avatar;
    }

    public void SetBoxImage (Texture image) {
        this.boxImage = image;
    }

    public Texture GetBoxImage() {
        return this.boxImage;
    }
}
