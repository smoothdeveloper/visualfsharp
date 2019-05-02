
neg_many_many_overloads.fsx(123,1,123,13): typecheck error FS0041: A unique overload for method 'A' could not be determined based on type information prior to this program point. A type annotation may be needed.



Argument given: 'a when 'a : null



Candidates:
 - static member Class.A : a:Dictionary<DateTime,DateTime> -> Dictionary<DateTime,DateTime>

 - static member Class.A : a:Dictionary<Task,Task> -> Dictionary<Task,Task>

 - static member Class.A : a:Dictionary<decimal,decimal> -> Dictionary<decimal,decimal>

 - static member Class.A : a:Dictionary<float,float> -> Dictionary<float,float>

 - static member Class.A : a:Dictionary<float32,float32> -> Dictionary<float32,float32>

 - static member Class.A : a:Dictionary<int16,int16> -> Dictionary<int16,int16>

 - static member Class.A : a:Dictionary<int32,int32> -> Dictionary<int32,int32>

 - static member Class.A : a:Dictionary<int64,int64> -> Dictionary<int64,int64>

 - static member Class.A : a:Dictionary<int8,int8> -> Dictionary<int8,int8>

 - static member Class.A : a:Dictionary<string,string> -> Dictionary<string,string>

 - static member Class.A : a:Dictionary<uint16,uint16> -> Dictionary<uint16,uint16>

 - static member Class.A : a:Dictionary<uint32,uint32> -> Dictionary<uint32,uint32>

 - static member Class.A : a:Dictionary<uint64,uint64> -> Dictionary<uint64,uint64>

 - static member Class.A : a:Dictionary<uint8,uint8> -> Dictionary<uint8,uint8>

 - static member Class.A : a:HashSet<DateTime> -> HashSet<DateTime>

 - static member Class.A : a:HashSet<Task> -> HashSet<Task>

 - static member Class.A : a:HashSet<decimal> -> HashSet<decimal>

 - static member Class.A : a:HashSet<float32> -> HashSet<float32>

 - static member Class.A : a:HashSet<float> -> HashSet<float>

 - static member Class.A : a:HashSet<int16> -> HashSet<int16>

 - static member Class.A : a:HashSet<int32> -> HashSet<int32>

 - static member Class.A : a:HashSet<int64> -> HashSet<int64>

 - static member Class.A : a:HashSet<int8> -> HashSet<int8>

 - static member Class.A : a:HashSet<string> -> HashSet<string>

 - static member Class.A : a:HashSet<uint16> -> HashSet<uint16>

 - static member Class.A : a:HashSet<uint32> -> HashSet<uint32>

 - static member Class.A : a:HashSet<uint64> -> HashSet<uint64>

 - static member Class.A : a:HashSet<uint8> -> HashSet<uint8>

 - static member Class.A : a:List<DateTime> -> List<DateTime>

 - static member Class.A : a:List<Task> -> List<Task>

 - static member Class.A : a:List<decimal> -> List<decimal>

 - static member Class.A : a:List<float32> -> List<float32>

 - static member Class.A : a:List<float> -> List<float>

 - static member Class.A : a:List<int16> -> List<int16>

 - static member Class.A : a:List<int32> -> List<int32>

 - static member Class.A : a:List<int64> -> List<int64>

 - static member Class.A : a:List<int8> -> List<int8>

 - static member Class.A : a:List<string> -> List<string>

 - static member Class.A : a:List<uint16> -> List<uint16>

 - static member Class.A : a:List<uint32> -> List<uint32>

 - static member Class.A : a:List<uint64> -> List<uint64>

 - static member Class.A : a:List<uint8> -> List<uint8>

 - static member Class.A : a:Queue<DateTime> -> Queue<DateTime>

 - static member Class.A : a:Queue<Task> -> Queue<Task>

 - static member Class.A : a:Queue<decimal> -> Queue<decimal>

 - static member Class.A : a:Queue<float32> -> Queue<float32>

 - static member Class.A : a:Queue<float> -> Queue<float>

 - static member Class.A : a:Queue<int16> -> Queue<int16>

 - static member Class.A : a:Queue<int32> -> Queue<int32>

 - static member Class.A : a:Queue<int64> -> Queue<int64>

 - static member Class.A : a:Queue<int8> -> Queue<int8>

 - static member Class.A : a:Queue<string> -> Queue<string>

 - static member Class.A : a:Queue<uint16> -> Queue<uint16>

 - static member Class.A : a:Queue<uint32> -> Queue<uint32>

 - static member Class.A : a:Queue<uint64> -> Queue<uint64>

 - static member Class.A : a:Queue<uint8> -> Queue<uint8>

 - static member Class.A : a:Task -> Task

 - static member Class.A : a:Task<DateTime> -> Task<DateTime>

 - static member Class.A : a:Task<Task> -> Task<Task>

 - static member Class.A : a:Task<decimal> -> Task<decimal>

 - static member Class.A : a:Task<float32> -> Task<float32>

 - static member Class.A : a:Task<float> -> Task<float>

 - static member Class.A : a:Task<int16> -> Task<int16>

 - static member Class.A : a:Task<int32> -> Task<int32>

 - static member Class.A : a:Task<int64> -> Task<int64>

 - static member Class.A : a:Task<int8> -> Task<int8>

 - static member Class.A : a:Task<string> -> Task<string>

 - static member Class.A : a:Task<uint16> -> Task<uint16>

 - static member Class.A : a:Task<uint32> -> Task<uint32>

 - static member Class.A : a:Task<uint64> -> Task<uint64>

 - static member Class.A : a:Task<uint8> -> Task<uint8>

 - static member Class.A : a:string -> string

neg_many_many_overloads.fsx(124,1,124,34): typecheck error FS0041: No overloads match for method 'A'.



Argument given: Type



Available overloads:
 - static member Class.A : a:DateTime -> DateTime

 - static member Class.A : a:Dictionary<DateTime,DateTime> -> Dictionary<DateTime,DateTime>

 - static member Class.A : a:Dictionary<Task,Task> -> Dictionary<Task,Task>

 - static member Class.A : a:Dictionary<decimal,decimal> -> Dictionary<decimal,decimal>

 - static member Class.A : a:Dictionary<float,float> -> Dictionary<float,float>

 - static member Class.A : a:Dictionary<float32,float32> -> Dictionary<float32,float32>

 - static member Class.A : a:Dictionary<int16,int16> -> Dictionary<int16,int16>

 - static member Class.A : a:Dictionary<int32,int32> -> Dictionary<int32,int32>

 - static member Class.A : a:Dictionary<int64,int64> -> Dictionary<int64,int64>

 - static member Class.A : a:Dictionary<int8,int8> -> Dictionary<int8,int8>

 - static member Class.A : a:Dictionary<string,string> -> Dictionary<string,string>

 - static member Class.A : a:Dictionary<uint16,uint16> -> Dictionary<uint16,uint16>

 - static member Class.A : a:Dictionary<uint32,uint32> -> Dictionary<uint32,uint32>

 - static member Class.A : a:Dictionary<uint64,uint64> -> Dictionary<uint64,uint64>

 - static member Class.A : a:Dictionary<uint8,uint8> -> Dictionary<uint8,uint8>

 - static member Class.A : a:HashSet<DateTime> -> HashSet<DateTime>

 - static member Class.A : a:HashSet<Task> -> HashSet<Task>

 - static member Class.A : a:HashSet<decimal> -> HashSet<decimal>

 - static member Class.A : a:HashSet<float32> -> HashSet<float32>

 - static member Class.A : a:HashSet<float> -> HashSet<float>

 - static member Class.A : a:HashSet<int16> -> HashSet<int16>

 - static member Class.A : a:HashSet<int32> -> HashSet<int32>

 - static member Class.A : a:HashSet<int64> -> HashSet<int64>

 - static member Class.A : a:HashSet<int8> -> HashSet<int8>

 - static member Class.A : a:HashSet<string> -> HashSet<string>

 - static member Class.A : a:HashSet<uint16> -> HashSet<uint16>

 - static member Class.A : a:HashSet<uint32> -> HashSet<uint32>

 - static member Class.A : a:HashSet<uint64> -> HashSet<uint64>

 - static member Class.A : a:HashSet<uint8> -> HashSet<uint8>

 - static member Class.A : a:List<DateTime> -> List<DateTime>

 - static member Class.A : a:List<Task> -> List<Task>

 - static member Class.A : a:List<decimal> -> List<decimal>

 - static member Class.A : a:List<float32> -> List<float32>

 - static member Class.A : a:List<float> -> List<float>

 - static member Class.A : a:List<int16> -> List<int16>

 - static member Class.A : a:List<int32> -> List<int32>

 - static member Class.A : a:List<int64> -> List<int64>

 - static member Class.A : a:List<int8> -> List<int8>

 - static member Class.A : a:List<string> -> List<string>

 - static member Class.A : a:List<uint16> -> List<uint16>

 - static member Class.A : a:List<uint32> -> List<uint32>

 - static member Class.A : a:List<uint64> -> List<uint64>

 - static member Class.A : a:List<uint8> -> List<uint8>

 - static member Class.A : a:Queue<DateTime> -> Queue<DateTime>

 - static member Class.A : a:Queue<Task> -> Queue<Task>

 - static member Class.A : a:Queue<decimal> -> Queue<decimal>

 - static member Class.A : a:Queue<float32> -> Queue<float32>

 - static member Class.A : a:Queue<float> -> Queue<float>

 - static member Class.A : a:Queue<int16> -> Queue<int16>

 - static member Class.A : a:Queue<int32> -> Queue<int32>

 - static member Class.A : a:Queue<int64> -> Queue<int64>

 - static member Class.A : a:Queue<int8> -> Queue<int8>

 - static member Class.A : a:Queue<string> -> Queue<string>

 - static member Class.A : a:Queue<uint16> -> Queue<uint16>

 - static member Class.A : a:Queue<uint32> -> Queue<uint32>

 - static member Class.A : a:Queue<uint64> -> Queue<uint64>

 - static member Class.A : a:Queue<uint8> -> Queue<uint8>

 - static member Class.A : a:Task -> Task

 - static member Class.A : a:Task<DateTime> -> Task<DateTime>

 - static member Class.A : a:Task<Task> -> Task<Task>

 - static member Class.A : a:Task<decimal> -> Task<decimal>

 - static member Class.A : a:Task<float32> -> Task<float32>

 - static member Class.A : a:Task<float> -> Task<float>

 - static member Class.A : a:Task<int16> -> Task<int16>

 - static member Class.A : a:Task<int32> -> Task<int32>

 - static member Class.A : a:Task<int64> -> Task<int64>

 - static member Class.A : a:Task<int8> -> Task<int8>

 - static member Class.A : a:Task<string> -> Task<string>

 - static member Class.A : a:Task<uint16> -> Task<uint16>

 - static member Class.A : a:Task<uint32> -> Task<uint32>

 - static member Class.A : a:Task<uint64> -> Task<uint64>

 - static member Class.A : a:Task<uint8> -> Task<uint8>

 - static member Class.A : a:decimal -> decimal

 - static member Class.A : a:float -> float

 - static member Class.A : a:float32 -> float32

 - static member Class.A : a:int16 -> int16

 - static member Class.A : a:int32 -> int32

 - static member Class.A : a:int64 -> int64

 - static member Class.A : a:int8 -> int8

 - static member Class.A : a:string -> string

 - static member Class.A : a:uint16 -> uint16

 - static member Class.A : a:uint32 -> uint32

 - static member Class.A : a:uint64 -> uint64

 - static member Class.A : a:uint8 -> uint8

neg_many_many_overloads.fsx(125,1,125,25): typecheck error FS0041: No overloads match for method 'A'.



Argument given: Guid



Available overloads:
 - static member Class.A : a:DateTime -> DateTime

 - static member Class.A : a:Dictionary<DateTime,DateTime> -> Dictionary<DateTime,DateTime>

 - static member Class.A : a:Dictionary<Task,Task> -> Dictionary<Task,Task>

 - static member Class.A : a:Dictionary<decimal,decimal> -> Dictionary<decimal,decimal>

 - static member Class.A : a:Dictionary<float,float> -> Dictionary<float,float>

 - static member Class.A : a:Dictionary<float32,float32> -> Dictionary<float32,float32>

 - static member Class.A : a:Dictionary<int16,int16> -> Dictionary<int16,int16>

 - static member Class.A : a:Dictionary<int32,int32> -> Dictionary<int32,int32>

 - static member Class.A : a:Dictionary<int64,int64> -> Dictionary<int64,int64>

 - static member Class.A : a:Dictionary<int8,int8> -> Dictionary<int8,int8>

 - static member Class.A : a:Dictionary<string,string> -> Dictionary<string,string>

 - static member Class.A : a:Dictionary<uint16,uint16> -> Dictionary<uint16,uint16>

 - static member Class.A : a:Dictionary<uint32,uint32> -> Dictionary<uint32,uint32>

 - static member Class.A : a:Dictionary<uint64,uint64> -> Dictionary<uint64,uint64>

 - static member Class.A : a:Dictionary<uint8,uint8> -> Dictionary<uint8,uint8>

 - static member Class.A : a:HashSet<DateTime> -> HashSet<DateTime>

 - static member Class.A : a:HashSet<Task> -> HashSet<Task>

 - static member Class.A : a:HashSet<decimal> -> HashSet<decimal>

 - static member Class.A : a:HashSet<float32> -> HashSet<float32>

 - static member Class.A : a:HashSet<float> -> HashSet<float>

 - static member Class.A : a:HashSet<int16> -> HashSet<int16>

 - static member Class.A : a:HashSet<int32> -> HashSet<int32>

 - static member Class.A : a:HashSet<int64> -> HashSet<int64>

 - static member Class.A : a:HashSet<int8> -> HashSet<int8>

 - static member Class.A : a:HashSet<string> -> HashSet<string>

 - static member Class.A : a:HashSet<uint16> -> HashSet<uint16>

 - static member Class.A : a:HashSet<uint32> -> HashSet<uint32>

 - static member Class.A : a:HashSet<uint64> -> HashSet<uint64>

 - static member Class.A : a:HashSet<uint8> -> HashSet<uint8>

 - static member Class.A : a:List<DateTime> -> List<DateTime>

 - static member Class.A : a:List<Task> -> List<Task>

 - static member Class.A : a:List<decimal> -> List<decimal>

 - static member Class.A : a:List<float32> -> List<float32>

 - static member Class.A : a:List<float> -> List<float>

 - static member Class.A : a:List<int16> -> List<int16>

 - static member Class.A : a:List<int32> -> List<int32>

 - static member Class.A : a:List<int64> -> List<int64>

 - static member Class.A : a:List<int8> -> List<int8>

 - static member Class.A : a:List<string> -> List<string>

 - static member Class.A : a:List<uint16> -> List<uint16>

 - static member Class.A : a:List<uint32> -> List<uint32>

 - static member Class.A : a:List<uint64> -> List<uint64>

 - static member Class.A : a:List<uint8> -> List<uint8>

 - static member Class.A : a:Queue<DateTime> -> Queue<DateTime>

 - static member Class.A : a:Queue<Task> -> Queue<Task>

 - static member Class.A : a:Queue<decimal> -> Queue<decimal>

 - static member Class.A : a:Queue<float32> -> Queue<float32>

 - static member Class.A : a:Queue<float> -> Queue<float>

 - static member Class.A : a:Queue<int16> -> Queue<int16>

 - static member Class.A : a:Queue<int32> -> Queue<int32>

 - static member Class.A : a:Queue<int64> -> Queue<int64>

 - static member Class.A : a:Queue<int8> -> Queue<int8>

 - static member Class.A : a:Queue<string> -> Queue<string>

 - static member Class.A : a:Queue<uint16> -> Queue<uint16>

 - static member Class.A : a:Queue<uint32> -> Queue<uint32>

 - static member Class.A : a:Queue<uint64> -> Queue<uint64>

 - static member Class.A : a:Queue<uint8> -> Queue<uint8>

 - static member Class.A : a:Task -> Task

 - static member Class.A : a:Task<DateTime> -> Task<DateTime>

 - static member Class.A : a:Task<Task> -> Task<Task>

 - static member Class.A : a:Task<decimal> -> Task<decimal>

 - static member Class.A : a:Task<float32> -> Task<float32>

 - static member Class.A : a:Task<float> -> Task<float>

 - static member Class.A : a:Task<int16> -> Task<int16>

 - static member Class.A : a:Task<int32> -> Task<int32>

 - static member Class.A : a:Task<int64> -> Task<int64>

 - static member Class.A : a:Task<int8> -> Task<int8>

 - static member Class.A : a:Task<string> -> Task<string>

 - static member Class.A : a:Task<uint16> -> Task<uint16>

 - static member Class.A : a:Task<uint32> -> Task<uint32>

 - static member Class.A : a:Task<uint64> -> Task<uint64>

 - static member Class.A : a:Task<uint8> -> Task<uint8>

 - static member Class.A : a:decimal -> decimal

 - static member Class.A : a:float -> float

 - static member Class.A : a:float32 -> float32

 - static member Class.A : a:int16 -> int16

 - static member Class.A : a:int32 -> int32

 - static member Class.A : a:int64 -> int64

 - static member Class.A : a:int8 -> int8

 - static member Class.A : a:string -> string

 - static member Class.A : a:uint16 -> uint16

 - static member Class.A : a:uint32 -> uint32

 - static member Class.A : a:uint64 -> uint64

 - static member Class.A : a:uint8 -> uint8

neg_many_many_overloads.fsx(126,1,126,48): typecheck error FS0041: No overloads match for method 'A'.



Argument given: DayOfWeek



Available overloads:
 - static member Class.A : a:DateTime -> DateTime

 - static member Class.A : a:Dictionary<DateTime,DateTime> -> Dictionary<DateTime,DateTime>

 - static member Class.A : a:Dictionary<Task,Task> -> Dictionary<Task,Task>

 - static member Class.A : a:Dictionary<decimal,decimal> -> Dictionary<decimal,decimal>

 - static member Class.A : a:Dictionary<float,float> -> Dictionary<float,float>

 - static member Class.A : a:Dictionary<float32,float32> -> Dictionary<float32,float32>

 - static member Class.A : a:Dictionary<int16,int16> -> Dictionary<int16,int16>

 - static member Class.A : a:Dictionary<int32,int32> -> Dictionary<int32,int32>

 - static member Class.A : a:Dictionary<int64,int64> -> Dictionary<int64,int64>

 - static member Class.A : a:Dictionary<int8,int8> -> Dictionary<int8,int8>

 - static member Class.A : a:Dictionary<string,string> -> Dictionary<string,string>

 - static member Class.A : a:Dictionary<uint16,uint16> -> Dictionary<uint16,uint16>

 - static member Class.A : a:Dictionary<uint32,uint32> -> Dictionary<uint32,uint32>

 - static member Class.A : a:Dictionary<uint64,uint64> -> Dictionary<uint64,uint64>

 - static member Class.A : a:Dictionary<uint8,uint8> -> Dictionary<uint8,uint8>

 - static member Class.A : a:HashSet<DateTime> -> HashSet<DateTime>

 - static member Class.A : a:HashSet<Task> -> HashSet<Task>

 - static member Class.A : a:HashSet<decimal> -> HashSet<decimal>

 - static member Class.A : a:HashSet<float32> -> HashSet<float32>

 - static member Class.A : a:HashSet<float> -> HashSet<float>

 - static member Class.A : a:HashSet<int16> -> HashSet<int16>

 - static member Class.A : a:HashSet<int32> -> HashSet<int32>

 - static member Class.A : a:HashSet<int64> -> HashSet<int64>

 - static member Class.A : a:HashSet<int8> -> HashSet<int8>

 - static member Class.A : a:HashSet<string> -> HashSet<string>

 - static member Class.A : a:HashSet<uint16> -> HashSet<uint16>

 - static member Class.A : a:HashSet<uint32> -> HashSet<uint32>

 - static member Class.A : a:HashSet<uint64> -> HashSet<uint64>

 - static member Class.A : a:HashSet<uint8> -> HashSet<uint8>

 - static member Class.A : a:List<DateTime> -> List<DateTime>

 - static member Class.A : a:List<Task> -> List<Task>

 - static member Class.A : a:List<decimal> -> List<decimal>

 - static member Class.A : a:List<float32> -> List<float32>

 - static member Class.A : a:List<float> -> List<float>

 - static member Class.A : a:List<int16> -> List<int16>

 - static member Class.A : a:List<int32> -> List<int32>

 - static member Class.A : a:List<int64> -> List<int64>

 - static member Class.A : a:List<int8> -> List<int8>

 - static member Class.A : a:List<string> -> List<string>

 - static member Class.A : a:List<uint16> -> List<uint16>

 - static member Class.A : a:List<uint32> -> List<uint32>

 - static member Class.A : a:List<uint64> -> List<uint64>

 - static member Class.A : a:List<uint8> -> List<uint8>

 - static member Class.A : a:Queue<DateTime> -> Queue<DateTime>

 - static member Class.A : a:Queue<Task> -> Queue<Task>

 - static member Class.A : a:Queue<decimal> -> Queue<decimal>

 - static member Class.A : a:Queue<float32> -> Queue<float32>

 - static member Class.A : a:Queue<float> -> Queue<float>

 - static member Class.A : a:Queue<int16> -> Queue<int16>

 - static member Class.A : a:Queue<int32> -> Queue<int32>

 - static member Class.A : a:Queue<int64> -> Queue<int64>

 - static member Class.A : a:Queue<int8> -> Queue<int8>

 - static member Class.A : a:Queue<string> -> Queue<string>

 - static member Class.A : a:Queue<uint16> -> Queue<uint16>

 - static member Class.A : a:Queue<uint32> -> Queue<uint32>

 - static member Class.A : a:Queue<uint64> -> Queue<uint64>

 - static member Class.A : a:Queue<uint8> -> Queue<uint8>

 - static member Class.A : a:Task -> Task

 - static member Class.A : a:Task<DateTime> -> Task<DateTime>

 - static member Class.A : a:Task<Task> -> Task<Task>

 - static member Class.A : a:Task<decimal> -> Task<decimal>

 - static member Class.A : a:Task<float32> -> Task<float32>

 - static member Class.A : a:Task<float> -> Task<float>

 - static member Class.A : a:Task<int16> -> Task<int16>

 - static member Class.A : a:Task<int32> -> Task<int32>

 - static member Class.A : a:Task<int64> -> Task<int64>

 - static member Class.A : a:Task<int8> -> Task<int8>

 - static member Class.A : a:Task<string> -> Task<string>

 - static member Class.A : a:Task<uint16> -> Task<uint16>

 - static member Class.A : a:Task<uint32> -> Task<uint32>

 - static member Class.A : a:Task<uint64> -> Task<uint64>

 - static member Class.A : a:Task<uint8> -> Task<uint8>

 - static member Class.A : a:decimal -> decimal

 - static member Class.A : a:float -> float

 - static member Class.A : a:float32 -> float32

 - static member Class.A : a:int16 -> int16

 - static member Class.A : a:int32 -> int32

 - static member Class.A : a:int64 -> int64

 - static member Class.A : a:int8 -> int8

 - static member Class.A : a:string -> string

 - static member Class.A : a:uint16 -> uint16

 - static member Class.A : a:uint32 -> uint32

 - static member Class.A : a:uint64 -> uint64

 - static member Class.A : a:uint8 -> uint8
