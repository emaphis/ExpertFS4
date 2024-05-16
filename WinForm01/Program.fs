open System
open System.Windows.Forms
open System.Drawing


let initializeComponent (frmInitialized : Form) =
    let currentOSVersion = Environment.OSVersion.Version.Major.ToString() + "." + Environment.Version.Minor.ToString() + "." + Environment.OSVersion.Version.ToString()
    let mutable btnHelloWinForm = new Button()
    btnHelloWinForm.Text <- "Hello F#"
    btnHelloWinForm.Location <- new Point(40, 100)
    btnHelloWinForm.Size <- new Size(180, 40)
    btnHelloWinForm.Click.Add(fun eventArgs -> MessageBox.Show("Runs on Windows Version " + currentOSVersion) |> ignore)
    frmInitialized.Controls.Add(btnHelloWinForm)



[<EntryPoint; STAThread>]
let main argv =
    let mainForm = new Form()
    let mainFormTitle = $"Hello from F#! My name is {nameof mainForm}"
    mainForm.Text <- mainFormTitle
    mainForm.Size <- new Size(500, 350)
    initializeComponent(mainForm)
    Application.Run(mainForm)
    0
