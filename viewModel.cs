using System;
namespace Interfaz
{


    public class ModelEventargs : EventArgs
    {
        public Bean miObjeto { get; private set; }

        public Bean(Bean miObjeto)
        {
            this.miObjeto = miObjeto;
        }

        public Bean()
        {
            this.miObjeto = null;
        }

    }

    public delegate void ModelEventHandler(object sender, ModelEventargs e);

    public class ViewModel
    {
        public event ModelEventHandler addFunction;
        public event ModelEventHandler deleteFunction;
        public event ModelEventHandler modifyFunction;

        private Model model;
        private static ViewModel viewModel = null;

        public static ViewModel getInstance()
        {
            if (null == viewModel)
                return new ViewModel();
            else
                return viewModel;
        }

        protected static virtual ViewModel()
        {
            model = new Model();
        }

        public void addFunction(Bean bean)
        {
            model.addFunction(bean);
            OnAddFunction(bean);
        }

        public void deleteFunction(Bean bean)
        {
            model.deleteFunction(bean);
            OnDeleteFunction(bean);
        }

        public void modifyFunction(Bean bean)
        {
            model.modifyFunction(bean);
            OnModifyFunction(bean);
        }

        protected virtual void OnAddFunction(Bean bean)
        {
            if (null != addFunction) addFunction(this, new ModelEventargs(bean));
        }

        protected virtual void OnDeleteFunction(Bean bean)
        {
            if (null != deleteFunction) deleteFunction(this, new ModelEventargs(bean));
        }

        protected virtual void OnModifyFunction(Bean bean)
        {
            if (null != modifyFunction) modifyFunction(this, new ModelEventargs(bean));
        }
    }
}
