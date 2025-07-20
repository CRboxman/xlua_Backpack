--首先应该先把json表从AB包中加载出来
--加载的Json文件 TextAsset对象
local txt = ABMgr: LoadRes("json", "ItemData", typeof(TextAsset))
--获取它的文本信息进行json解析
local itemlist = Json.decode(txt.text)
--加载出来是一个像数组结构的数据
--不方便我们通过id去获取里面的内容所以我们用一张新表转存一次
--而且这张新的道具表在任何地方都能够被使用
--一张用来存储道具信息的表
--键值对形式键是道具ID值是道具表一行信息
ItemData = {}
for _, value in pairs(itemlist) do 
    ItemData[value.id] = value
end
