using System.ComponentModel;
using System.Text.Json;

namespace Mandelbrot;

public partial class InfoForm : Form
{
    readonly JsonSerializerOptions _jsonClipboardOptions = new() { WriteIndented = true };

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CenterX { get => labelCenterX.Text; set => labelCenterX.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CenterY { get => labelCenterY.Text; set => labelCenterY.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string MouseX { get => labelMouseX.Text; set => labelMouseX.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string MouseY { get => labelMouseY.Text; set => labelMouseY.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string WindowW { get => labelWindowW.Text; set => labelWindowW.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string WindowH { get => labelWindowH.Text; set => labelWindowH.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Zoom { get => labelZoom.Text; set => labelZoom.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Iterations { get => labelIterations.Text; set => labelIterations.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Perturbation { get => labelPerturbation.Text; set => labelPerturbation.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Renderer { get => labelGPU.Text; set => labelGPU.Text = value; }

    public InfoForm()
    {
        InitializeComponent();
    }

    void OnCopyClicked(object sender, EventArgs e) => Clipboard.SetText(JsonSerializer.Serialize(new Dictionary<string, string>
        {
            ["Center X"] = CenterX,
            ["Center Y"] = CenterY,
            ["Zoom"] = Zoom,
            ["Iterations"] = Iterations,
            ["Window W"] = WindowW,
            ["Window H"] = WindowH,
            ["Perturbation"] = Perturbation,
            ["Renderer"] = Renderer
        }, options: _jsonClipboardOptions));
}
