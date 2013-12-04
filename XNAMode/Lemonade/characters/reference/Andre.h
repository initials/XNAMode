#import "BaseCharacter.h"

@interface Andre : BaseCharacter {
    
}

+ (id) andreWithOrigin:(CGPoint)Origin;
- (id) initWithOrigin:(CGPoint)Origin;

- (void)bounceOffEnemy;


@end
