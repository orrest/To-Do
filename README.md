<h2>前端</h2>
<h3>功能性</h3>
<ul>
<li>主页面
<ul>
<li>[x] 点击用户栏导航到登录对话框</li>
<li>[ ] 用户信息展示</li>
<li>[ ] 搜索框</li>
<li>[x] 导航栏</li>
<li>[x] 登录检测
<ul>
<li>[x] 如果没有token，打开登录对话框</li>
<li>[x] 如果存在token文件，自动登录</li>
</ul>
</li>
<li>[x] 登录</li>
<li>[x] 注册</li>
</ul>
</li>
<li>Loading bar
<ul>
<li>[x] 将初始化移到导航条的生命周期函数中</li>
<li>[x] TaskCollectionViewModel</li>
<li>[x] CountdownViewModel</li>
<li>[x] StaredViewModel</li>
<li>[x] TaskDrawerViewModel</li>
</ul>
</li>
<li>分页
<ul>
<li>[x] 分页功能</li>
<li>[x] 测试前后页</li>
<li>[x] 测试多页数据刷新</li>
</ul>
</li>
<li>Task页面
<ul>
<li>[x] 读取任务
<ul>
<li>[x] Tasks属性</li>
<li>[x] 读取到VM并显示</li>
</ul>
</li>
<li>[x] 添加任务
<ul>
<li>[x] 通过API插入数据库</li>
<li>[x] 插入到VM并在前端显示</li>
</ul>
</li>
<li>[x] 更新任务
<ul>
<li>[x] Star</li>
<li>[x] Finish</li>
<li>[x] Task Description
<ul>
<li>[x] 失去焦点后按回车键 Keyboard.ClearFocus()</li>
</ul>
</li>
</ul>
</li>
<li>[x] 缓存抽屉：由Task的VM决定，在TaskVM中使用字典进行缓存，key是taskId，value是drawerVM的引用</li>
<li>[x] 月度页面</li>
<li>[x] 长期任务页面</li>
</ul>
</li>
<li>任务抽屉页面
<ul>
<li>[x] 添加步骤
<ul>
<li>[x] Steps属性</li>
<li>[x] 添加步骤在drawerVM中进行，因为添加是在抽屉中进行的操作</li>
</ul>
</li>
<li>[x] 删除步骤
<ul>
<li>[x] 删除按钮在TaskStepViewModel中，因为删除按钮属于ListBoxItem</li>
<li>[x] 在删除时更新抽屉中的VM</li>
</ul>
</li>
<li>[x] 添加备注</li>
<li>[x] 修改备注</li>
<li>[ ] 重构，删除外部的所谓缓存，直接在每个Task的vm中自己持有自己的drawer内容。外部也不需要selectedtask</li>
</ul>
</li>
<li>TaskStepVm
<ul>
<li>[x] 删除</li>
<li>[x] 更新
<ul>
<li>[x] Finish</li>
<li>[x] 更新任意部分内容</li>
</ul>
</li>
</ul>
</li>
<li>标记星页面
<ul>
<li>[x] 显示所有被标记星的内容</li>
<li>[ ] 跨页面VM更新（EventAggregator）</li>
</ul>
</li>
<li>倒计时页面
<ul>
<li>[x] material:Expander未完成</li>
<li>[x] Progressbar无法自动填充Grid
<ul>
<li>[x] 确保 HorizontalContentAlignment="Stretch" 设置到ListBox 中</li>
</ul>
</li>
<li>[x] 创建页面的对话框 Prism</li>
<li>[x] 创建后端服务
<ul>
<li>[x] Model</li>
<li>[x] DTO &lt;- ViewModel</li>
</ul>
</li>
<li>[x] 进入页面时的GetAsync</li>
<li>[x] 创建对话框
<ul>
<li>[x] 切换 天数/日期，相应切换右侧控件</li>
<li>[x] 添加图标选择</li>
<li>[x] 添加描述填写</li>
<li>[x] 点击确认后，在数据库中添加新的countdown，并从Dialog返回并添加到集合中</li>
<li>[x] 选择日期时更新目标日期</li>
<li>[x] 添加倒计时Tab时初始化集合</li>
</ul>
</li>
</ul>
</li>
</ul>
<h3>优化</h3>
<ul>
<li>[ ] Polly网络重试</li>
<li>[ ] 动画</li>
<li>[ ] Dialog: Elevation不显示，可能是DialogWindow截取了多余的边框</li>
<li>[ ] Validation</li>
</ul>
<h3>上架</h3>
<ul>
<li>[ ] 上架到微软商店</li>
</ul>
<h3>单元测试</h3>
<ul>
<li>[ ] TODO</li>
</ul>
<h2>后端</h2>
<h3>重构</h3>
<ul>
<li>Service
<ul>
<li>[x] 移除IService、Service，直接使用Controller</li>
</ul>
</li>
<li>分页接口修改
<ul>
<li>[x] PagedList</li>
<li>[x] Tasks</li>
<li>[x] TaskSteps</li>
<li>[x] Countdowns</li>
</ul>
</li>
</ul>
<h3>ToDoTasks</h3>
<ul>
<li>[x] material:Expander未完成</li>
<li>[x] 进度条无法自动充满Grid
<ul>
<li>[x] Did you set HorizontalContentAlignment="Stretch" 设置给ListBox</li>
</ul>
</li>
<li>[x] 对话框的创建 Prism</li>
<li>[x] 创建后端服务
<ul>
<li>[x] Entity, dto, service, controller, dbcontext</li>
</ul>
</li>
<li>[x] 更新本地迁移</li>
<li>[x] 测试增删改查</li>
</ul>
<h3>集成测试</h3>
<ul>
<li>[ ] TODO</li>
</ul>
</div></div>
