using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour
{
    public Transform player;//����ٴ� player����  
    void Update()
    {
        gameObject.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);//ī�޶� player�� ����ٴϰ� �Ѵ�.  
        //ī�޶��� z��ġ�� ���߷��� ��󺸴� �ڿ��־�� �ϴϱ� ��ü������ z���� �ش�.
    }
}

// ī�޶� �ִ밪, �ּڰ�
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class camera : MonoBehaviour
//{
//    public Transform player;
//    public float xMin;
//    public float yMin;
//    public float xMax;
//    public float yMax;
//    void Update()
//    {
//        float x = Mathf.Clamp(player.position.x, xMin, xMax);//� ���� �ִ밪�� �ּڰ��� �����ִ� �Լ�.
//        float y = Mathf.Clamp(player.position.y, yMin, yMax);
//        gameObject.transform.position = new Vector3(x, y, this.transform.position.z);
//        //ī�޶��� z��ġ�� ���߷��� ��󺸴� �ڿ��־�� �ϴϱ� ��ü������ z���� �ش�.
//    }

//}