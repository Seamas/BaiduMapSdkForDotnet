## 使用指南

## 扩展
### 返回类型 Response

返回类型分为四类，具体请根据接口文档，分析返回数据，继承相应的类

T为相应的返回数据结构

+ BaiduResponse
+ BaiduSingleResponse<T>
+ BaiduListResultResponse<T>
+ BaiduListResultsResponse<T>


### 请求类型 Request

T为返回类型Response，S为请求类型Model的数据结构

+ BaiduRequest<T, S>

### 新增接口

+ 新增Response.Models类，新增Response类
+ 新增Request.Models类，新增Request类


## 调用

```csharp
var client = new BaiduClient(ak, sk);
```