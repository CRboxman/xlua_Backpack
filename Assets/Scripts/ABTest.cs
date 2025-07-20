using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ABTest 
{
    public string mainBundleName = "PC"; // ����������


    private static ABTest instance = new ABTest();
    public static ABTest Instance
    {
        get { return instance; }
    }
    private ABTest()
    {

    }
    private AssetBundle bundle=null;
    private AssetBundleManifest manifest=null;
    private Dictionary<string,AssetBundle> ABDic = new Dictionary<string,AssetBundle>();

    private string StrPath
    {
        get { return Application.streamingAssetsPath + "/";  }
    }
    /// <summary>
    /// ֱ�Ӷ�ȡ�����ܵ���ͬ������
    /// </summary>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <returns></returns>
    public Object LoadRes(string ABbao,string WenJian)
    {
        //��Ϊ�ղ�����       ����
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("����δ�ҵ����������ƻ����޸�mainBundleName");
                return null;
            }
        }
        //�������������      �̶��ļ������������ȡ����
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //����Ƿ���������     ������     ��û�������ӵ�����ֵ䣬��ֹ�ظ�
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("����������ʧ�ܣ�" + str[i] + "\n" + "\tδ�ҵ����������������ab��/�����İ����Ƿ���ȷ");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //����Ƿ����������
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("����Դ������ʧ�ܣ�" + ABbao + "\n" + "\t����ab��/�����İ����Ƿ���ȷ");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        return ABDic[ABbao].LoadAsset(WenJian);
    }
    /// <summary>
    /// ָ��һ�����ͣ���ת����һ���̶ȼ�����ͬ������
    /// </summary>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadRes(string ABbao, string WenJian,System.Type type)
    {
        //��Ϊ�ղ�����       ����
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("����δ�ҵ����������ƻ����޸�mainBundleName");
                return null;
            }
        }
        //�������������      �̶��ļ������������ȡ����
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //����Ƿ���������     ������     ��û�������ӵ�����ֵ䣬��ֹ�ظ�
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("����������ʧ�ܣ�" + str[i] + "\n" + "\tδ�ҵ����������������ab��/�����İ����Ƿ���ȷ");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //����Ƿ����������
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("����Դ������ʧ�ܣ�" + ABbao + "\n" + "\t����ab��/�����İ����Ƿ���ȷ");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        return ABDic[ABbao].LoadAsset(WenJian,type);
    }
    /// <summary>
    /// ���÷��ͷ�ʽ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <returns></returns>
    public T LoadRes<T>(string ABbao, string WenJian) where T : Object 
    {
        //��Ϊ�ղ�����       ����
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("����δ�ҵ����������ƻ����޸�mainBundleName");
                return null;
            }
        }
        //�������������      �̶��ļ������������ȡ����
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //����Ƿ���������     ������     ��û�������ӵ�����ֵ䣬��ֹ�ظ�
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("����������ʧ�ܣ�" + str[i]+"\n"+ "\tδ�ҵ����������������ab��/�����İ����Ƿ���ȷ");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //����Ƿ��������ab��
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("����Դ������ʧ�ܣ�" + ABbao+ "\n"+"\t����ab��/�����İ����Ƿ���ȷ");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        T asset = ABDic[ABbao].LoadAsset<T>(WenJian);
        if (asset == null)
        {
            Debug.Log("��Դ�����ڣ�" + WenJian+"\n"+"\t������Դ�����Ƿ���ȷ");
        }

        return asset;
    }
    public void RemoveRes(string ABbao)
    {
        ABDic[ABbao].Unload(false);
        ABDic.Remove(ABbao);
    }
    public void RemoveAllRes() 
    {
        AssetBundle.UnloadAllAssetBundles(false);
        ABDic.Clear();
        bundle=null;
        manifest=null;
    }
}
