//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DataManager : MonoBehaviour
//{

//    // ����ũ�� ID�� �ϳ� �����.
//    // todo : ���� üũ�ؾ���
//    string FindUniqueID()
//    {
//        string id;
//        id = "Mirim" + UnityEngine.Random.Range(0, 1000000);

//        return id;
//    }

//    GameManager.Instance.GetPlugin().StorageSave("BestScore", BestScore, false, (state, message, rawData, dictionary) {
//        if (state.Equals(Configure.PN_API_STATE_SUCCESS))
//            Debug.Log("������ ���� �Ϸ�");
//        else
//            Debug.Log("������ ���� ����");

//    }

//   public Plugin GetPlugin()
//    {
//        // playnanoo plugin
//        Plugin plugin = Plugin.GetInstance();
//        if (StaticData.ID == "")
//        {
//            Debug.Assert(false);
//        }
//        plugin.SetUUID(StaticData.ID);
//        plugin.SetLanguage("Configure.PN_LANG_KO");
//        return plugin;
//    }

//}
