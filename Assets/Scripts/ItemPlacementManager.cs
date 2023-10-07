using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementManager : MonoBehaviour
{
    private static ItemPlacementManager _instance;
    public static ItemPlacementManager Instance { get { return _instance; } }

    // 아이템 아이콘을 배치하는 메서드
    public void PlaceItemIcon(Vector3 position, ItemSO itemData)
    {
        // 아이콘 프리팹을 생성하고 위치를 설정
        GameObject itemIcon = Instantiate(itemData.iconPrefab, position, Quaternion.identity);

        // 생성된 아이콘을 원하는 위치에 배치
        // 예를 들어 UI Canvas 하위에 배치하거나 다른 방식으로 화면에 표시할 수 있습니다.
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // 기타 메서드 및 필드 추가 가능
}
