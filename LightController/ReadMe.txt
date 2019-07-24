0.需要用到的几个外部dll:
	①NHibernate4最后版本；
	②FTD2XX :调用DMX512设备的驱动
	③IrisSkin4 ：皮肤包

1.几个文件夹及文件夹说明
	①Ast：存放的是一些辅助的包装类；用于数据打包或临时变量的存储
	②Common：存放一些使用工具类；
	③DAO：数据库连接相关的几个类
	④Entity：数据库对象类
	⑤Mapping：存放数据库与类映射相关的配置文件
	⑥MyForm：存放所有的Form类
	⑦Source：存放一些用到的静态资源、或相关类库文件
	⑧Tools：曾维佳写的调用DMX512相关的工具类
	⑨hibernate.cfg.xml：NHibernate总配置文件	