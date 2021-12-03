# 89Test03技术文档

1、功能概述

- 实现玩家排行榜功能，并在排行榜上方实现赛季倒计时，点击不同玩家item，弹出对应的提示信息
- 涉及知识点：OSA滚动插件的使用，Json数据的读取，时间戳及委托的使用



2、资源目录结构

| 目录名称  | 目录内容               | 父目录  | 其他说明     |
| --------- | ---------------------- | ------- | ------------ |
| Resources | 存放动态加载需要的资源 |         | Assets根目录 |
| Plugins   | OSA用于优化滚动列表    | Plugins | ---          |
| Scripts   | 存放启动器等脚本信息   | Assets  |              |



3、界面对象结构拆分

| 结构             | 结构对象说明                   | 父界面对象  | 其他说明 |
| ---------------- | ------------------------------ | ----------- | -------- |
| SampleScene      | 主场景                         |             |          |
| GameManager      | 游戏界面父物体，管理者脚本挂载 |             |          |
| LeaderBoardPanel | 主界面父物体用于管理           | GameManager |          |
| StartBtn         | 游戏启动按钮                   | Canvas      |          |



4、脚本功能部分

| 类                 | 主要职责           | 其他说明Main           |
| ------------------ | ------------------ | ---------------------- |
| GameController     | 用于启动游戏       | 读数据，初始化界面信息 |
| UILeaderBoardModel | 界面对应数据管理类 | 用于保存排行榜数据     |
| RankItemPanelView  | 排行榜列表界面     | 用于处理列表展示信息   |
| TipPanelView       | 点击玩家提示界面   | 用于处理所要展示的信息 |
| BasiclistAdapter   | 滚动列表基类       | 用于优化列表           |



5、数据解析

- 创建对应类，通过SimpleJson对数据进行解析处理，并保存与Model类中。

  | 玩家ID | 名字     | 奖杯数 |
  | ------ | -------- | ------ |
  | UID    | NickName | Trophy |
  
  

6、部分功能的实现思想

- 启动器初始化（进行读区数据），存于Model类中
- 数据赋值成功后，打开界面，进行列表的信息赋值
- 逻辑数据分离细想，进行View层与MC的分开处理



7、关键代码逻辑的流程图

![Image](https://github.com/89trillion-songjunbo/89Test03_New/blob/main/89Test03New%20脚本流程图.png)


8、运行流程图
![Image](https://github.com/89trillion-songjunbo/89Test03/blob/master/89Test03.png)



