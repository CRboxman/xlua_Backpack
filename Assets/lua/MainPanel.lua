--只要是一个新的对象(面板)我们那就新建一张表
MainPanel ={}
--不是必须写因为1ua的特性不存在声明变量的概念
--这样写的目的是当别人看这个1ua代码时知道这个表(对象)有什么变量很重要 --关联的面板对象
MainPanel.panelObj = nil
--对应的面板控件
MainPanel.btnRole = nil
MainPanel.btnSkill = nil
--需要做实例化面板对象
--为这个面板处理对应的逻辑比如按钮点击等等
--初始化该面板实例化对象控件事件监听
function MainPanel: Init()
end