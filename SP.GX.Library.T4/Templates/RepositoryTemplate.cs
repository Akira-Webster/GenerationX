using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// VisualStudio Intergration
using EnvDTE;
using EnvDTE80;
// T4 Toolbox
using T4Toolbox;
// SP.GX
using SP.GX.Library.Generation;
using SP.GX.Library.T4.Generators;
using SP.GX.Library.T4.Utilities;

namespace SP.GX.Library.T4.Templates
{
    public class RepositoryTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".repos.cs";

            this.WriteLine("using System;");
            this.WriteLine("using System.Linq;");
            this.WriteLine("using System.Collections.Generic;");
            this.WriteLine("using System.Text;");
            this.WriteLine("// SharePoint 14");
            this.WriteLine("using Microsoft.SharePoint;");
            this.WriteLine("// SPGenesis Framework http://spgenesis.codeplex.com/");
            this.WriteLine("using SPGenesis.Core;");
            this.WriteLine("using SPGenesis.Entities;");
            this.WriteLine("// SharePoint GenerationX");
            this.WriteLine("using SP.GX.Library.Generation.Interfaces;");

            this.WriteLine("");

            foreach (var @namespace in this.Model.CodeElements.OfType<CodeNamespace>())
            {
                foreach (var entityClass in @namespace.Children.OfType<CodeClass2>())
                {
                    var className = Inflector.Pluralize(entityClass.Name) + "Repository";

                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        this.WriteLine(string.Format("public interface IGeneric{0} : IInitializeRepository", className));
                        using (ScopeController.Begin(this))
                        {
                            // GetByID
                            this.WriteLine(string.Format("{0} GetById(int id);", entityClass.Name));
                            // GetByQuery
                            this.WriteLine(string.Format("IQueryable<{0}> GetByQuery();", entityClass.Name));
                            // Create
                            this.WriteLine(string.Format("{0} Create(Action<{0}> populate);", entityClass.Name));
                            // Update
                            this.WriteLine(string.Format("void Update({0} entity);", entityClass.Name));
                            // Delete
                            this.WriteLine(string.Format("void Delete({0} entity);", entityClass.Name));
                        }

                        this.WriteLine(string.Format("public class Generic{0} : IGeneric{0}, IInitializeRepository", className));
                        using (ScopeController.Begin(this))
                        {
                            this.WriteLine("public SPList List {get; protected set;}");
                            this.WriteLine("");

                            // Initialize option 1
                            this.WriteLine("public void Initialize(Microsoft.SharePoint.SPWeb web)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("this.List = {0}.Instance.TryGetList(web);", entityClass.Name + "List");
                            }

                            // Initialize option 2
                            this.WriteLine("public void Initialize(string webUrl)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("var manager = {0}.Instance.GetList(webUrl, SPGENListInstanceGetMethod.ByUrl);", entityClass.Name + "List");
                                this.WriteLine("if(manager != null)");
                                using (ScopeController.Begin(this))
                                {
                                    this.WriteLine("this.List = manager.List;");
                                }
                            }

                            // GetByID
                            this.WriteLine(string.Format("public {0} GetById(int id)", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("return SPGENEntityManager<{0}, {1}>.Instance.GetEntity(this.List, id);", entityClass.Name, entityClass.Name + "Mapper");
                            }

                            // GetByQuery
                            this.WriteLine(string.Format("public IQueryable<{0}> GetByQuery()", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("return SPGENEntityManager<{0}, {1}>.Instance.GetQueryableList(this.List);", entityClass.Name, entityClass.Name + "Mapper");
                            }

                            // Create
                            this.WriteLine(string.Format("public {0} Create(Action<{0}> populate)", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("var entity = new {0}();", entityClass.Name);
                                this.WriteLine("populate(entity);", entityClass.Name);
                                this.WriteLine("SPGENEntityManager<{0}, {1}>.Instance.CreateNewListItem(entity, this.List);", entityClass.Name, entityClass.Name + "Mapper");
                                this.WriteLine("return entity;");
                            }

                            // Update
                            this.WriteLine(string.Format("public void Update({0} entity)", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("SPGENEntityManager<{0}, {1}>.Instance.UpdateListItem(entity, this.List);", entityClass.Name, entityClass.Name + "Mapper");
                            }

                            // Delete
                            this.WriteLine(string.Format("public void Delete({0} entity)", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("SPGENEntityManager<{0}, {1}>.Instance.DeleteListItem(entity, this.List);", entityClass.Name, entityClass.Name + "Mapper");
                            }
                        }
                    }
                }
            }

            return this.GenerationEnvironment.ToString();
        }
    }
}
