window.VegaHelper = {
    setup: function (props) {
        var view;

        function render(spec, data, containerSelector, height) 
        {
            view = new vega.View(vega.parse(spec), {
                renderer:  'svg',  // renderer (canvas or svg)
                container: containerSelector,   // parent DOM container
                hover:     true,       // enable hover processing
            });
            // view.run();
            let changeset = vega.changeset().remove(() => true).insert(data);
            view.height(height).run()
            return view.change("table", changeset).run()
        }

        render(JSON.parse(props.spec), JSON.parse(props.data), props.containerSelector, props.height);
    },

}