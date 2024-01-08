using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class BaseComponent
{
    public virtual string Type { get; set; }
    public string Id { get; set; }
    public decimal MarginTop { get; set; }
    public decimal MarginRight { get; set; }
    public decimal MarginBottom { get; set; }
    public decimal MarginLeft { get; set; }
    public decimal PaddingTop { get; set; }
    public decimal PaddingRight { get; set; }
    public decimal PaddingBottom { get; set; }
    public decimal PaddingLeft { get; set; }
    public bool EnableBackgrondColor { get; set; }
    public string BackgroundColor { get; set; }
    public bool EnableBackgroundPattern { get; set; }
    public string BackgroundPattern { get; set; }
    public decimal BorderWidth { get; set; }
    public string BorderStyle { get; set; }
    public string BorderColor { get; set; }
    public decimal BorderRadius { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
}