﻿using System;
using UnityEngine.EventSystems;

namespace CharacterEditor
{
    public class PrevMeshColorBtn : MeshAndTextureTypeMaskSelector, IPointerClickHandler
    {
        private Action updateMeshCalback;

        private void TextureChangeHandler(object sender, EventArgs e)
        {
            TextureManager.Instance.OnTextureChanged -= TextureChangeHandler;
            updateMeshCalback.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TextureManager.Instance.OnTextureChanged -= TextureChangeHandler;

            updateMeshCalback = () =>
            {
                if (meshTypes.Length > 0)
                {
                    if (textureTypes.Length > 0)
                    {
                        MeshManager.Instance.SetMeshColor(meshTypes,
                            TextureManager.Instance.currentCharacterTextures[textureTypes[0]].SelectedColor);
                    }
                    else
                    {
                        MeshManager.Instance.OnPrevColor(meshTypes);
                    }
                }
            };

            if (textureTypes.Length > 0)
            {
                TextureManager.Instance.OnPrevColor(textureTypes);
                TextureManager.Instance.OnTextureChanged += TextureChangeHandler;
            }
            else
            {
                updateMeshCalback.Invoke();
            }
        }
    }
}