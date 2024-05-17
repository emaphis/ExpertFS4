// Classes - Static Members

// Static Initializers

type ClassWithStaticCtor() =
     static let mutable staticField = 0
     static do printfn "Invoking static initializer"
               staticField <- 10
     do printfn "Static Field Value: %i" staticField


// Static Fields

module Logger =
    let private log l c m = printfn "%-5s [%s] %s" l c m
    let LogInfo = log "INFO"
    let LogError = log "ERROR"

type MyService() =
    static let logCategory = "MyService"
    member x.DoSomething() =
        Logger.LogInfo logCategory "Doing something"
    member x.DoSomethingElse() =
        Logger.LogError logCategory "Doing something else"


let svc = MyService()

svc.DoSomething()
svc.DoSomethingElse()


// Static Properties

type Processor() =
    static let mutable itemsProcessed = 0
    static member ItemsProcessed = itemsProcessed
    member x.Process() =
        itemsProcessed <- itemsProcessed + 1
        printfn "Processing..."

do while Processor.ItemsProcessed < 5 do (Processor()).Process()


// Static Methods

open System.IO

[<AbstractClass>]
type ImageReader() =
  abstract member Dimensions : int * int with get
  abstract member Resolution : int * int with get
  abstract member Content : byte array with get

type JpgImageReader(fileName : string) =
  inherit ImageReader()
  override x.Dimensions with get() = (0, 0)
  override x.Resolution with get() = (0, 0)
  override x.Content with get() = Array.empty<byte>

type GifImageReader(fileName : string) =
  inherit ImageReader()
  override x.Dimensions with get() = (0, 0)
  override x.Resolution with get() = (0, 0)
  override x.Content with get() = Array.empty<byte>

type PngImageReader(fileName : string) =
  inherit ImageReader()
  override x.Dimensions with get() = (0, 0)
  override x.Resolution with get() = (0, 0)
  override x.Content with get() = Array.empty<byte>

[<Sealed>]
type ImageReaderFactory private() =
  static member CreateReader(fileName) =
    let fi = FileInfo(fileName)
    match fi.Extension.ToUpper() with
    | ".JPG" -> JpgImageReader(fileName) :> ImageReader
    | ".GIF" -> GifImageReader(fileName) :> ImageReader
    | ".PNG" -> PngImageReader(fileName) :> ImageReader
    | ext -> failwith (sprintf "Unsupported extension: %s" ext)

ImageReaderFactory.CreateReader "MyPicture.jpg"
ImageReaderFactory.CreateReader "MyPicture.gif"
ImageReaderFactory.CreateReader "MyPicture.png"
ImageReaderFactory.CreateReader "MyPicture.targa"
