using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Threading;

public class FileDownloadTest : MonoBehaviour
{
    //public string url;
    //public string cookie;
    //public string fileName;
    //public string fileType;
    //IEnumerator DownloadFile()
    //{
    //    string filePath = $"{Application.persistentDataPath}/{fileName}";
    //    if ((fileType != null) && (fileType.Trim() != ""))
    //    {
    //        filePath += $".{fileType}";
    //    }
    //    UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
    //    if (string.IsNullOrWhiteSpace(cookie) == false)
    //    {
    //        unityWebRequest.SetRequestHeader("Cookie", cookie);
    //    }
    //    yield return unityWebRequest.SendWebRequest();
    //    if (unityWebRequest.isNetworkError)
    //    {
    //        Debug.LogError(unityWebRequest.error);
    //    }
    //    else
    //    {
    //        Debug.Log(unityWebRequest.downloadHandler.text);
    //        //File.WriteAllBytes(filePath, unityWebRequest.downloadHandler.data);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadFile());
    }

    string url = "https://www.my-server.com/image.png";
    string fileName = "file";
    string fileType = ".png";


    IEnumerator DownloadFile()
    {
        string filePath = $"{Application.persistentDataPath}/{fileName}{fileType}";

        Debug.Log($"Download:{url}");
        {
        DOWNLOAD_RETRY:;
            {
                var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
                var operation = unityWebRequest.SendWebRequest();
                yield return new WaitUntil(() => operation.isDone);

                if (unityWebRequest.error != null)
                {
                    Debug.LogError(unityWebRequest.error);
                    yield return new WaitForSeconds(3f);
                    goto DOWNLOAD_RETRY;
                }
                else
                {
                    Debug.Log(filePath);

                    var folderPath = System.IO.Path.GetDirectoryName(filePath);
                    Debug.Log(folderPath);
                    if (Directory.Exists(folderPath) == false)//폴더가 없으면 생성
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    File.WriteAllBytes(filePath, unityWebRequest.downloadHandler.data);
                }

                unityWebRequest.Dispose();
            }
        }
        yield return filePath;


    }

    List<string> downloadList = new List<string>();
    IEnumerator DownloadFileV3(string downloadlink, string filePath)
    {
        downloadList.Add(downloadlink);

        DOWNLOADRETRY:;
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(downloadlink);
            unityWebRequest.downloadHandler = new DownloadHandlerFile(filePath);

            var operation = unityWebRequest.SendWebRequest();
            yield return new WaitUntil(() => operation.isDone);

            if (unityWebRequest.error != null)
            {
                Debug.LogError(unityWebRequest.error);
                yield return new WaitForSeconds(1f);
                goto DOWNLOADRETRY;
            }
            else
            {
                Debug.Log(filePath);
            }
        }
        downloadList.Remove(downloadlink);
    }

    static AudioClip LoadAudioClip(string downloadLink)
    {
        var fileName = downloadLink;
        if (File.Exists(downloadLink))
        {
            downloadLink = $"file://{downloadLink}";
            fileName = Path.GetFileNameWithoutExtension(downloadLink);
        }
        UnityWebRequest unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(downloadLink, AudioType.UNKNOWN);

        var operation = unityWebRequest.SendWebRequest();
        while (!operation.isDone)
        {
            Thread.Sleep(1);
        }
        if (unityWebRequest.error != null)
        {
            Debug.LogError(unityWebRequest.error);
        }
        else
        {
            //Debug.Log("LoadAudioClipSuccess");
        }
        var clip = DownloadHandlerAudioClip.GetContent(unityWebRequest);
        clip.name = fileName;
        return clip;
    }

    public string urlSample = "https://wmmu.tistory.com/";
    IEnumerator DownloadHTML(string url)
    {
        UnityWebRequest unityWebRequest;
        unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.error != null)
        {
            Debug.LogError(unityWebRequest.error);
        }
        else
        {
            Debug.Log(unityWebRequest.downloadHandler.text);
        }
    }
}
