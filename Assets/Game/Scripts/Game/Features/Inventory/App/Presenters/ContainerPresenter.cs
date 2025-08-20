using System;
using Game.Features.Inventory.Domain;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class ContainerPresenter : IInitializable, IDisposable
    {
        private readonly Mediator _mediator;
        private readonly IContainerView _view;
        private readonly ItemContainer _model;

        public ContainerPresenter(IContainerView view, ItemContainer model, Mediator mediator)
        {
            _view = view;
            _model = model;
            _mediator = mediator;
        }

        public void Initialize()
        {
            _mediator.Registry.Register(this);
            
            _model.OnContentChanged += UpdateModelView;
            UpdateModelView();

            _view.OnHovered += ReportHovered;
            _view.OnUnhovered += ReportUnhovered;
        }

        private void ReportUnhovered() => 
            _mediator.OnContainerUnhovered(this);

        private void ReportHovered() => 
            _mediator.OnContainerHovered(this);

        private void UpdateModelView()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _view.OnHovered -= ReportHovered;
            _view.OnUnhovered -= ReportUnhovered;
            _mediator.Registry.Unregister(this);
        }

        public void GetProjection()
        {
            _model.AcceptVisitor(new ItemProjectionInquirer());
        }
    }
}