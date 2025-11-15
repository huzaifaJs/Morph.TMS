using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Morpho.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.GeoFencing
{
    public class GeoFenceZone : FullAuditedAggregateRoot<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public string Name { get; protected set; }
        public GeoFenceShapeType ShapeType { get; protected set; }

        // For circle
        public double? CenterLatitude { get; protected set; }
        public double? CenterLongitude { get; protected set; }
        public double? RadiusMeters { get; protected set; }

        // For polygon / rectangle: store GeoJSON / custom JSON
        public string PolygonCoordinatesJson { get; protected set; }

        public bool IsActive { get; protected set; }

        protected GeoFenceZone() { }

        public GeoFenceZone(int tenantId, string name, GeoFenceShapeType shapeType)
        {
            TenantId = tenantId;
            Name = name;
            ShapeType = shapeType;
            IsActive = true;
        }

        public void Deactivate() => IsActive = false;

        public void SetCircle(double lat, double lng, double radiusMeters)
        {
            ShapeType = GeoFenceShapeType.Circle;
            CenterLatitude = lat;
            CenterLongitude = lng;
            RadiusMeters = radiusMeters;
            PolygonCoordinatesJson = null;
        }

        public void SetPolygon(string polygonJson)
        {
            ShapeType = GeoFenceShapeType.Polygon;
            PolygonCoordinatesJson = polygonJson;
            CenterLatitude = null;
            CenterLongitude = null;
            RadiusMeters = null;
        }
    }
}
