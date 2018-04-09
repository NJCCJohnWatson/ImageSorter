# ImageSorter
Write by VS2017 and C# in WPF platfom,use it to sort picture

#图片分类器
在VS2017上使用基于C#的WPF平台编写
能够将横板和竖版的图片给分类出来
并在选择分类文件夹的下分别新建Hor(横板)和Ver(竖版)文件夹，来存放图片。

是晚上被声音吵醒睡不着，写出来的。
引用了Windows.Forms作为打开文件管理器的方式，使用系统IO在新建文件夹和复制文件，使用Bitmap位图来读取图片信息。
目前缺陷有:
1.只能读取图片文件，遇到其他文件无法跳过，并报错退出。所以只能在纯图片文件夹下使用。
2.方法都在在Click事件下，过于臃肿。

