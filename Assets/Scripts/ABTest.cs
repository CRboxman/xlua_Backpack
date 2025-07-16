using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ABTest 
{
    public string mainBundleName = "PC"; // 主包的名字


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
    /// 直接读取，可能导致同名问题
    /// </summary>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <returns></returns>
    public Object LoadRes(string ABbao,string WenJian)
    {
        //包为空才添加       主包
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("主包未找到，请检查名称或者修改mainBundleName");
                return null;
            }
        }
        //加载这个主包的      固定文件，用于下面获取依赖
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //检查是否加载了相关     依赖包     ，没有则添加到这个字典，防止重复
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("依赖包加载失败：" + str[i] + "\n" + "\t未找到相关依赖包，请检查ab包/主包的包名是否正确");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //检测是否加载了主包
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("主资源包加载失败：" + ABbao + "\n" + "\t请检查ab包/主包的包名是否正确");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        return ABDic[ABbao].LoadAsset(WenJian);
    }
    /// <summary>
    /// 指定一个类型，并转换，一定程度减少了同名问题
    /// </summary>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadRes(string ABbao, string WenJian,System.Type type)
    {
        //包为空才添加       主包
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("主包未找到，请检查名称或者修改mainBundleName");
                return null;
            }
        }
        //加载这个主包的      固定文件，用于下面获取依赖
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //检查是否加载了相关     依赖包     ，没有则添加到这个字典，防止重复
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("依赖包加载失败：" + str[i] + "\n" + "\t未找到相关依赖包，请检查ab包/主包的包名是否正确");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //检测是否加载了主包
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("主资源包加载失败：" + ABbao + "\n" + "\t请检查ab包/主包的包名是否正确");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        return ABDic[ABbao].LoadAsset(WenJian,type);
    }
    /// <summary>
    /// 常用泛型方式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ABbao"></param>
    /// <param name="WenJian"></param>
    /// <returns></returns>
    public T LoadRes<T>(string ABbao, string WenJian) where T : Object 
    {
        //包为空才添加       主包
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(StrPath + mainBundleName);
            if (bundle == null)
            {
                Debug.Log("主包未找到，请检查名称或者修改mainBundleName");
                return null;
            }
        }
        //加载这个主包的      固定文件，用于下面获取依赖
        manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] str = manifest.GetAllDependencies(ABbao);
        AssetBundle ab = null;
        for (int i = 0; i < str.Length; i++)
        {
            //检查是否加载了相关     依赖包     ，没有则添加到这个字典，防止重复
            if (!ABDic.ContainsKey(str[i]))
            {
                ab = AssetBundle.LoadFromFile(StrPath + str[i]);
                if (ab == null)
                {
                    Debug.Log("依赖包加载失败：" + str[i]+"\n"+ "\t未找到相关依赖包，请检查ab包/主包的包名是否正确");
                    continue;
                }
                ABDic.Add(str[i], ab);
            }
        }
        //检测是否加载了主ab包
        if (!ABDic.ContainsKey(ABbao))
        {
            ab = AssetBundle.LoadFromFile(StrPath + ABbao);
            if (ab == null)
            {
                Debug.Log("主资源包加载失败：" + ABbao+ "\n"+"\t请检查ab包/主包的包名是否正确");
                return null;
            }
            ABDic.Add(ABbao, ab);
        }
        T asset = ABDic[ABbao].LoadAsset<T>(WenJian);
        if (asset == null)
        {
            Debug.Log("资源不存在：" + WenJian+"\n"+"\t请检查资源名称是否正确");
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
