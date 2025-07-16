using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;



//MyLoaderAB 那里改ab包名
public class LuaMgr : BaseManager<LuaMgr>
{
    private LuaEnv luaEnv;
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }

    private string LuaAssetPath_No_txt= "/"+"lua/"; // Lua文件的路径，注意这里是相对于Assets目录的路径
    private string ABbaoName_txt = "lua";        // // AB包的名字

    /// <summary>
    /// 加载Lua文件
    /// </summary>
    /// <param name="filename"></param>
    public void DoLuaFile(string filename)
    {
        string str = string.Format("require('{0}')", filename);
        DoString(str);
    }
    /// <summary>
    /// 初始化Lua环境，并添加自定义的Lua重定向加载器
    /// </summary>
    public void Init()
    {
        if (luaEnv != null)
            return;
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.AddLoader(MyLoaderAB);
    }
    /// <summary>
    /// 重定向asset文件夹下的lua文件的加载方式
    /// 不需要改后缀
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] MyLoader(ref string filepath)
    {
        string path = Application.dataPath + LuaAssetPath_No_txt + filepath + ".lua";   
        //Debug.Log(path);//  具体的文件的路径
        //Debug.Log(filepath);//    require的文件名
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
    }
    /// <summary>
    /// 重定向AssetBundle文件夹下的lua文件的加载方式，需要加txt后缀！！！！！！！！
    /// 需要改为txt后缀
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] MyLoaderAB(ref string filepath)
    {
        #region 传统方式
        //string path = Application.streamingAssetsPath + "/lua";
        //AssetBundle ab = AssetBundle.LoadFromFile(path);

        //TextAsset textAsset = ab.LoadAsset<TextAsset>(filepath + ".lua");
        //return textAsset.bytes;
        #endregion
        #region 使用自己写的AB包管理器
        TextAsset luaTextAsset = ABTest.Instance.LoadRes<TextAsset>(ABbaoName_txt, filepath + ".lua");///需要更改ab包名！！！！！！！
        if (luaTextAsset != null)
            return luaTextAsset.bytes;
        return null;
        #endregion
    }
    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("未实例化解析器");
        }
        luaEnv.DoString(str);
    }
    public void Tick()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();
        }
        else
        {
            Debug.Log("未实例化解析器");
        }
    }
    public void Dispose()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            luaEnv = null;
        }
        else
        {
            Debug.Log("未实例化解析器");
        }
    }
}
