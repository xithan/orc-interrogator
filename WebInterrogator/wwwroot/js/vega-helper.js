function createView(view, spec, containerSelector, height) {
    view = new vega.View(vega.parse(spec), {
        renderer: 'svg',  // renderer (canvas or svg)
        container: containerSelector,   // parent DOM container
        hover: true,       // enable hover processing
    });

    view.height(height)
    return view;
}

window.VegaHelper = {
    setupGraph: function (props) {
        var view;

        function render(spec, nodes, edges, containerSelector, height)
        {
            view = createView(view, spec, containerSelector, height);

            let nodeChangeset = vega.changeset().remove(() => true).insert(nodes);
            view.change("nodes", nodeChangeset)
            let edgeChangeset = vega.changeset().remove(() => true).insert(edges);
            view.change("edges", edgeChangeset)
            return view.run()
        }

        render(JSON.parse(props.spec), JSON.parse(props.nodes), JSON.parse(props.edges), 
            props.containerSelector, props.height);
    },
    
    setupTable: function (props) {
        var view;

        function render(spec, data, containerSelector, height)
        {
            view = createView(view, spec, containerSelector, height);

            let changeset = vega.changeset().remove(() => true).insert(data);
            view.change("table", changeset)
            return view.run()
        }

        render(JSON.parse(props.spec), JSON.parse(props.data), props.containerSelector, props.height);
    },
    
    
    setup: function (props) {
        var view;

        function render(spec, data, containerSelector, height) 
        {
            view = createView(view, spec, containerSelector, height);

            view.height(height).run()
            for (const [name, dataset] of Object.entries(data)) {
                console.log(name, dataset);
                let changeset = vega.changeset().remove(() => true).insert(dataset);
                view.change(name, changeset)
            }
            return view.run()
        }

        render(JSON.parse(props.spec), JSON.parse(props.data), props.containerSelector, props.height);
    },

}