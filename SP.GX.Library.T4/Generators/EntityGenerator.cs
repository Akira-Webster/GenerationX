using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// T4 Toolbox
using T4Toolbox;
// Visual Studio Automation
using EnvDTE80;
// GenerationX 
using SP.GX.Library.T4.Templates;

//http://rulesengine.codeplex.com/ - For Bussiness Rules

namespace SP.GX.Library.T4.Generators
{
    public class EntityGenerator : Generator
    {
        public string File { get; set; }

        protected ITemplate TemplateForField = new FieldTemplate();
        protected ITemplate TemplateForContentType = new ContentTypeTemplate();
        protected ITemplate TemplateForEvents = new EventsTemplate();
        protected ITemplate TemplateForList = new ListTemplate();
        protected ITemplate TemplateForMapper = new MapperTemplate();
        protected ITemplate TemplateForRepository = new RepositoryTemplate();
        protected ITemplate TemplateForFeature = new FeatureTemplate();
        protected ITemplate TemplateForService = new ServiceTemplate();

        protected override void RunCore()
        {
            var projectItem = TransformationContext.FindProjectItem(this.File);
            var codeModel = projectItem.FileCodeModel as FileCodeModel2;

            if (this.TemplateForField != null)
            {
                this.TemplateForField.Generator = this;
                this.TemplateForField.Model = codeModel;
                this.TemplateForField.Render();
            }

            if (this.TemplateForContentType != null)
            {
                this.TemplateForContentType.Generator = this;
                this.TemplateForContentType.Model = codeModel;
                this.TemplateForContentType.Render();
            }

            if (this.TemplateForEvents != null)
            {
                this.TemplateForEvents.Generator = this;
                this.TemplateForEvents.Model = codeModel;
                this.TemplateForEvents.Render();
            }

            if (this.TemplateForList != null)
            {
                this.TemplateForList.Generator = this;
                this.TemplateForList.Model = codeModel;
                this.TemplateForList.Render();
            }

            if (this.TemplateForMapper != null)
            {
                this.TemplateForMapper.Generator = this;
                this.TemplateForMapper.Model = codeModel;
                this.TemplateForMapper.Render();
            }

            if (this.TemplateForRepository != null)
            {
                this.TemplateForRepository.Generator = this;
                this.TemplateForRepository.Model = codeModel;
                this.TemplateForRepository.Render();
            }

            if (this.TemplateForService != null)
            {
                this.TemplateForService.Generator = this;
                this.TemplateForService.Model = codeModel;
                this.TemplateForService.Render();
            }

            if (this.TemplateForFeature != null)
            {
                this.TemplateForFeature.Generator = this;
                this.TemplateForFeature.Model = codeModel;
                this.TemplateForFeature.Render();
            }
        }

        public void PrintInfo(StringBuilder buffer)
        {
            // Clear buffer
            buffer.Remove(0, buffer.Length);

            // Write Header
            buffer.AppendLine("Generation Information");
            buffer.AppendLine("Warning these file where generated automagically and any changes will be overwritten");

            // Date
            buffer.AppendLine(string.Format("Date: {0}", DateTime.Now));

            // Author
            buffer.AppendLine(string.Format("Author: {0}", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
        }

        public void Build(Type type)
        {

        }
    }
}
