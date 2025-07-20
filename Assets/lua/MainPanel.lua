--只要是一个新的对象(面板)我们那就新建一张表
MainPanel = {}
--不是必须写因为1ua的特性不存在声明变量的概念
--这样写的目的是当别人看这个1ua代码时知道这个表(对象)有什么变量很重要 --关联的面板对象
MainPanel.panelObj = nil
--对应的面板控件
MainPanel.btnRole = nil
MainPanel.btnSkill = nil
--需要做实例化面板对象
--为这个面板处理对应的逻辑比如按钮点击等等
--初始化该面板实例化对象控件事件监听
function MainPanel:Init()
    --1.实例化面板对象ABMgr+设置对象
    local prefab = ABMgr:LoadRes('ui', 'MainPanel', typeof(GameObject))
    self.panelObj = GameObject.Instantiate(prefab)
    self.panelObj.name = "MainPanel"  -- 直接设置为原始名称
    self.panelObj.transform:SetParent(Canvas, false)
    --2.找到对应控件
    --找到子对象再找到身上挂在的想要的脚本
    self.btnRole = self.panelObj.transform:Find('btnRole'):GetComponent(typeof(Button))
    --3.为控件加上事件监听进行点击等等的逻辑处理
    self.btnRole.onClick:AddListener(
        function()
            self:BtnRoleClick()
        end
    )
end

function MainPanel:ShowMe()
    self:Init()
    self.panelObj:SetActive(true)
end
function MainPanel:HideMe()
    self.panelObj:SetActive(false)
end

function MainPanel:BtnRoleClick()
    BagPanel:ShowMe()
end
