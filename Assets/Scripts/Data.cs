using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Com.Houdini.OneTap
{
    public class Data : MonoBehaviour
    {
        public static void SaveProfile(ProfileData t_profile)
        {
            try
            {
                string path = Application.persistentDataPath + "/profile.dt";
                FileStream file = File.Create(path);

                if (File.Exists(path)) File.Delete(path);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, t_profile);
                file.Close();
                Debug.Log("SAVE SUCCESSFUL");
            }
            catch
            {
                Debug.Log("SOMETHING WENT TERRIBLY WRONG WITH SAVING");
            }
        }

        public static ProfileData LoadProfile()
        {
            ProfileData ret = new ProfileData();
            try
            {
                string path = Application.persistentDataPath + "/profile.dt";

                if (File.Exists(path))
                {
                    FileStream file = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    ret = (ProfileData)bf.Deserialize(file);
                    Debug.Log("LOAD SUCCESSFUL");
                }
                
            }
            catch
            {
                Debug.Log("LOADIGN SAVE WENT WRONG");
            }
            return ret;
        }
    }
}

