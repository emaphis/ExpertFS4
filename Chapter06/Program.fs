open System
open System.Windows.Forms


// Using Optional Property Settings

let form =
        let tmp = new Form()
        tmp.Visible <- true
        tmp.TopMost <- true
        tmp.Text <- "Welcome to F#"
        tmp


open System.Drawing

type LabelInfoWithPropertySetting() =
        let mutable text = ""  // the default
        let mutable font = new Font(FontFamily.GenericSansSerif, 12.0f)
        member x.Text with get() = text and set v = text <- v
        member x.Font with get() = font and set v = font <- v

let labelInfo = LabelInfoWithPropertySetting(Text = "Hello World")


[<EntryPoint; STAThread>]
let main argv =
    Application.Run(form)
    0
