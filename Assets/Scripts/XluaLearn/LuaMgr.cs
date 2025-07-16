using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;



//MyLoaderAB �����ab����
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

    private string LuaAssetPath_No_txt= "/"+"lua/"; // Lua�ļ���·����ע�������������AssetsĿ¼��·��
    private string ABbaoName_txt = "lua";        // // AB��������

    /// <summary>
    /// ����Lua�ļ�
    /// </summary>
    /// <param name="filename"></param>
    public void DoLuaFile(string filename)
    {
        string str = string.Format("require('{0}')", filename);
        DoString(str);
    }
    /// <summary>
    /// ��ʼ��Lua������������Զ����Lua�ض��������
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
    /// �ض���asset�ļ����µ�lua�ļ��ļ��ط�ʽ
    /// ����Ҫ�ĺ�׺
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] MyLoader(ref string filepath)
    {
        string path = Application.dataPath + LuaAssetPath_No_txt + filepath + ".lua";   
        //Debug.Log(path);//  ������ļ���·��
        //Debug.Log(filepath);//    require���ļ���
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
    }
    /// <summary>
    /// �ض���AssetBundle�ļ����µ�lua�ļ��ļ��ط�ʽ����Ҫ��txt��׺����������������
    /// ��Ҫ��Ϊtxt��׺
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] MyLoaderAB(ref string filepath)
    {
        #region ��ͳ��ʽ
        //string path = Application.streamingAssetsPath + "/lua";
        //AssetBundle ab = AssetBundle.LoadFromFile(path);

        //TextAsset textAsset = ab.LoadAsset<TextAsset>(filepath + ".lua");
        //return textAsset.bytes;
        #endregion
        #region ʹ���Լ�д��AB��������
        TextAsset luaTextAsset = ABTest.Instance.LoadRes<TextAsset>(ABbaoName_txt, filepath + ".lua");///��Ҫ����ab������������������
        if (luaTextAsset != null)
            return luaTextAsset.bytes;
        return null;
        #endregion
    }
    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("δʵ����������");
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
            Debug.Log("δʵ����������");
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
            Debug.Log("δʵ����������");
        }
    }
}
