Ext.define('App.model.Track', {
    extend: 'Ext.data.Model',
    idProperty: 'TrackId',
    fields: [
        { name: 'TrackId', type: 'int' },
        { name: 'Name', type: 'string' },
        { name: 'AlbumId', type: 'int' }
    ], 
    //manyToMany: 'Playlist',
    proxy: {            // Note that proxy is defined in the Model, not the Store
        type: 'rest',
        url: '../../../api/TracksAssociationsManyToManyExample',
        reader: {
            type: 'json',
            rootProperty: 'tracks',
            totalProperty: 'count'
        }
    }
});