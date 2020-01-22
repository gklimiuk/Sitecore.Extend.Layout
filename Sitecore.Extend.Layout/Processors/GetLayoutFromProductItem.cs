using Sitecore.Mvc.Pipelines.Response.GetXmlBasedLayoutDefinition;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Data;

namespace Sitecore.Extend.Layout.Processors
{
    public class GetLayoutFromProductItem: GetFromLayoutField
    {
        private readonly ID ProductItemTemplateID = new ID("{5BF76A03-BDF0-42E7-AC2C-FF410D67AD0C}");
        private readonly ID ProductTypeTemplateID = new ID("{8FDDC51F-7BC8-43B0-8E00-0A4DDF4F1E2A}");
        private readonly ID ProductModelTemplateID = new ID("{82A72612-E405-4D23-A625-02CEB754DAC4}");        

        public override void Process(GetXmlBasedLayoutDefinitionArgs args)
        {
            if (args.Result != null)
                return;

            Item obj = args.ContextItem ?? PageContext.Current.Item;
            if (obj.TemplateID != ProductModelTemplateID
                && obj.TemplateID != ProductTypeTemplateID)
                {
                    return;
                }
            else
            {
                while (obj != null && obj.TemplateID != ProductItemTemplateID)
                {
                    obj = obj.Parent;
                }
                if (obj.TemplateID == ProductItemTemplateID)
                    args.Result = this.GetFromField(obj);
            }
        }

        //protected override XElement GetFromField(Item item)
        //{
        //    LayoutField layoutField = new LayoutField(item);
        //    if (layoutField.InnerField == null)
        //        return (XElement)null;
        //    string innerXml = layoutField.Data.InnerXml;
        //    if (innerXml.IsWhiteSpaceOrNull())
        //        return (XElement)null;
        //    return XDocument.Parse(innerXml).Root;
        //}
    }
}