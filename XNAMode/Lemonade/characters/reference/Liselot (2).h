#import "BaseCharacter.h"

@interface Liselot : BaseCharacter {
    
}

+ (id) liselotWithOrigin:(CGPoint)Origin;
- (id) initWithOrigin:(CGPoint)Origin;
- (void)bounceOffEnemy;

@end
