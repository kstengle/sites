using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Ektron.Cms.Controls;
using Ektron.Cms.API;
using Ektron.Cms;
/// <summary>
/// Summary description for BuildXML
/// </summary>
/// 
public class BuildXML
{
    private XmlDocument _xml;

    
    public XmlDocument xml
    {
        get
        {
            return _xml;
        }

    }


	public BuildXML()
	{
        _xml = new XmlDocument();
		//
		// TODO: Add constructor logic here
		//
	}

     
    public void CreateDeclaration()
    {
        XmlDeclaration dec = _xml.CreateXmlDeclaration("1.0", "utf-8", null);
        _xml.AppendChild(dec);
    }

    public XmlElement CreateRootElement(string Name)
    {

        XmlElement root = _xml.CreateElement(Name);
        _xml.AppendChild(root);
        return root;
    }
    public void CreateCSSInstructions(string xslfile)
    {
        XmlProcessingInstruction newPI;
        String PItext = "type='text/css' href='" + xslfile + "'";
        newPI = _xml.CreateProcessingInstruction("xml-stylesheet", PItext);
        _xml.AppendChild(newPI);
    }
    public XmlElement CreateEmptyNode(string Name, XmlElement appendXML)
    {
        XmlElement EmpyNode = _xml.CreateElement(Name);
        //if(appendXML.IsEmpty == false){
            
        //}else{
            appendXML.AppendChild(EmpyNode);
        //}
        
        return EmpyNode;
    }

    public void CreateEndNode(string NodeName, string NodeValue, XmlElement ParentNode)
    {
        XmlElement CreateNode = _xml.CreateElement(NodeName);
        if (NodeValue.Length>0)
        {
            CreateNode.InnerText = NodeValue;
        }    
            ParentNode.AppendChild(CreateNode);

    }
    public void CreateCDataNode(string NodeName, string NodeValue, XmlElement ParentNode)
    {
        XmlElement CreateNode = _xml.CreateElement(NodeName);
        XmlCDataSection CdataSection = _xml.CreateCDataSection(NodeValue);


        CreateNode.AppendChild(CdataSection);
        ParentNode.AppendChild(CreateNode);

    }
    public void AddAttribute(XmlElement NodeGettingAttribute, string AttributeName, string AttributeValue)
    {
        XmlAttribute AttributeToAdd = _xml.CreateAttribute(AttributeName);
        AttributeToAdd.InnerText = AttributeValue;
        NodeGettingAttribute.Attributes.Append(AttributeToAdd);
    }
}
