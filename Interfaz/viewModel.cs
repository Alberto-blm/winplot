using System;
namespace Interfaz
{


    public class ModelEventargs : EventArgs
    {
        public Bean miObjeto { get; set; }
        public int posicion;


        public ModelEventargs(Bean miObjeto, int posicion)
        {
            this.miObjeto = miObjeto;
            this.posicion = posicion;
        }

        public ModelEventargs()
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
        public struct limites
        {
            public double xMin, yMin, xMax, yMax;
        }
        limites real;
        private Model model;
        private static ViewModel viewModel = null;

        public Model Model { get => model; set => model = value; }
        public limites Real { get => real; set => real = value; }

        public static ViewModel getInstance()
        {
            if (null == viewModel)
                return new ViewModel();
            else
                return viewModel;
        }

        public  ViewModel()
        {
            model = new Model();
            real.xMax = 7;
            real.yMax = 7;
            real.xMin = -7;
            real.yMin = -7;
        }

        public void AddFunction(Bean bean)
        {
            model.addFunction(bean);
            OnAddFunction(bean);
        }

        public void DeleteFunction(Bean bean, int pos)
        {
            model.deleteFunction(bean);
            OnDeleteFunction(bean, pos);
        }

        public void ModifyFunction(Bean beanV, Bean beanN, int pos)
        {
            model.modifyFunction(beanV, beanN);
            OnModifyFunction(beanN, pos);
        }

        protected virtual void OnAddFunction(Bean bean)
        {
            if (null != addFunction) addFunction(this, new ModelEventargs(bean, -1));
        }

        protected virtual void OnDeleteFunction(Bean bean, int posicion)
        {
            if (null != deleteFunction) deleteFunction(this, new ModelEventargs(bean, posicion));
        }

        protected virtual void OnModifyFunction(Bean bean, int posicion)
        {
            if (null != modifyFunction) modifyFunction(this, new ModelEventargs(bean, posicion));
        }
    }
}
